using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignSupportDemo.Utilities.Error;
using System.Net.Http;
using System.Text;
using SignSupportDemo.SignSupport.API;
using SignSupportDemo.Utilities.DB;
using SignSupportDemo.Utilities.File;
using SignSupportDemo.Utilities.Form;
using SignSupportDemo.Utilities.Json;
using SignSupportDemo.Utilities.Multipart;
using SignSupportDemo.Utilities.Net;

namespace SignSupportDemo.Pages
{
    public class SignModel : PageModel
    {
        [BindProperty]
        public SignFormData SignFormData { get; set; }

        public SignRequestGenerationResponse SignRequestGenerationResponse { get; set; }

        public GeneralError GeneralError { get; set; }

        public SignSupportError SignSupportError { get; set; }

        public void OnGet()
        {
            SignFormData = SignFormDataFactory.Create();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<MultipartUploadMedia> UploadMedias = GetMultipartUploadMedias();
                    if (ModelState.IsValid)
                    {
                        List<TbsData> TbsDatas = await GetTbsDataAsync<List<TbsData>>(UploadMedias);
                        SignRequestGenerationResponse = await GetSignRequestAsync<SignRequestGenerationResponse>(TbsDatas);

                        // Save new sign-request in database
                        List<DocumentStorageObject> DocumentStorageObjects = DocumentStorageObjectFactory.Create(UploadMedias);
                        SignStorageObject SignStorageObject = SignStorageObjectFactory.Create(DocumentStorageObjects, TbsDatas, SignRequestGenerationResponse);
                        SignSupportDatabase.Upsert(SignStorageObject);
                    }
                }
                catch (SignSupportException Error)
                {
                    SignSupportError = Error.SignSupportError;
                }
                catch (Exception Error)
                {
                    GeneralError = ErrorHelper.GetError(Error);
                }
            }
            return Page();
        }

        private async Task<T> GetTbsDataAsync<T>(List<MultipartUploadMedia> uploadMedias)
        {
            string url = Startup.Configuration["SignSupport:Url"] + "/generator/tbsdata";
            HttpResponseMessage HttpResponse = await MultipartUploader.Upload(url, uploadMedias);
            return await TryParseResponseAsync<T>(HttpResponse);
        }

        private async Task<T> GetSignRequestAsync<T>(List<TbsData> tbsDatas)
        {
            SignFormData.Request.TbsDatas = tbsDatas;
            String requestJson = Encoding.UTF8.GetString(JsonUtil.JsonSerialize<SignRequestGenerationRequest>(SignFormData.Request));
            StringContent requestContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
            string url = Startup.Configuration["SignSupport:Url"] + "/generator/signrequest";
            HttpClient httpClient = GetSignRequestHttpClient();
            HttpResponseMessage HttpResponse = await httpClient.PostAsync(url, requestContent);
            return await TryParseResponseAsync<T>(HttpResponse);
        }

        private HttpClient GetSignRequestHttpClient()
        {
            HttpClient httpClient = new HttpClient();
            bool UseOrigin = Convert.ToBoolean(Startup.Configuration["SignSupport:UseOrigin"]);
            if (UseOrigin && Request.IsHttps)
            {
                string origin = "https://" + Request.Host;
                httpClient.DefaultRequestHeaders.Add("Origin", origin);
            }
            return httpClient;
        }

        private async Task<T> TryParseResponseAsync<T>(HttpResponseMessage response)
        {
            byte[] ResponseBody = await NetHelper.TryReadContentAsync(response);
            return JsonUtil.JsonDeserialize<T>(ResponseBody);
        }

        private List<MultipartUploadMedia> GetMultipartUploadMedias()
        {
            List<MultipartUploadMedia> uploadMedias = new List<MultipartUploadMedia>();
            foreach (var file in SignFormData.Documents)
            {
                byte[] bytes = Encoding.UTF8.GetBytes(file.FileName);
                byte[] data = FileHelpers.ProcessFormFile(file, ModelState);
                MultipartUploadMedia uploadMedia = new MultipartUploadMedia(data, "document", file.FileName);
                uploadMedias.Add(uploadMedia);
            }
            return uploadMedias;
        }
    }
}