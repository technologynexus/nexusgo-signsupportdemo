using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignSupportDemo.Utilities.DB;
using SignSupportDemo.Utilities.Multipart;
using SignSupportDemo.Utilities.Error;
using SignSupportDemo.SignSupport.API;
using SignSupportDemo.Utilities.Json;
using SignSupportDemo.Utilities.Net;

namespace SignSupportDemo.Pages
{
    [IgnoreAntiforgeryToken(Order = 1001)]
    public class SignResponseModel : PageModel
    {
        [BindProperty]
        public SignResponseData SignResponseData { get; set; }

        public GeneralError GeneralError { get; set; }

        public SignSupportError SignSupportError { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                GeneralError = ErrorHelper.GetError("Failed to complete signing, invalid response!");
                return Page();
            }
            try
            {
                SignStorageObject SignStorageObject = SignStorageObjectFinder.Find(SignResponseData.RelayState);
                SignCompletionRequest SignCompletionRequest = GetSignCompletionRequest(SignResponseData, SignStorageObject);
                string RequestJson = Encoding.UTF8.GetString(JsonUtil.JsonSerialize<SignCompletionRequest>(SignCompletionRequest));

                // Perform complete-signing request for each document
                foreach (var DocumentStorageObject in SignStorageObject.Documents)
                {
                    byte[] OriginalDocument = DocumentStorageObject.OriginalDocument;
                    MultipartUploadMedia UploadMedia = new MultipartUploadMedia(OriginalDocument, "document", DocumentStorageObject.FileName);
                    byte[] SignedDocumentBytes = await CompleteDocumentSignatureAsync(UploadMedia, RequestJson);
                    Console.WriteLine("File {0} had SignedDocumentBytes length {1}", DocumentStorageObject.FileName, SignedDocumentBytes.Length);

                    // Store signed document in database
                    DocumentStorageObject.SignedDocument = SignedDocumentBytes;
                }
                SignSupportDatabase.Upsert(SignStorageObject);
                return Redirect("~/SignResult?id=" + SignResponseData.RelayState);
            }
            catch (SignSupportException Error)
            {
                SignSupportError = Error.SignSupportError;
            }
            catch (Exception Error)
            {
                GeneralError = ErrorHelper.GetError(Error);
            }
            return Page();
        }

        private async Task<byte[]> CompleteDocumentSignatureAsync(MultipartUploadMedia uploadMedia, string requestJson)
        {
            string Url = Startup.Configuration["SignSupport:Url"] + "/completer/document/signature";
            HttpResponseMessage HttpResponse = await MultipartUploader.Upload(Url, new List<MultipartUploadMedia> { uploadMedia }, requestJson);
            return await TryParseResponseAsync(HttpResponse);
        }

        private async Task<byte[]> TryParseResponseAsync(HttpResponseMessage response)
        {
            byte[] ResponseBody = await NetHelper.TryReadContentAsync(response);
            if (ResponseBody != null)
            {
                return ResponseBody;
            }
            throw new Exception("Invalid response!", new Exception("Got invalid response from URL " + Url));
        }

        private SignCompletionRequest GetSignCompletionRequest(SignResponseData signResponseData, SignStorageObject signStorageObject)
        {
            return new SignCompletionRequest
            {
                EidSignResponse = signResponseData.EidSignResponse,
                RelayState = signResponseData.RelayState,
                TbsDatas = signStorageObject.TbsDatas
            };
        }
    }
}