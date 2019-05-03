using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignSupportDemo.Utilities.DB;
using SignSupportDemo.Utilities.Error;

namespace SignSupportDemo.Pages
{
    public class DownloadModel : PageModel
    {
        public GeneralError GeneralError { get; set; }

        public FileResult OnGet(string id, string file)
        {
            try
            {
                SignStorageObject signStorageObject = SignStorageObjectFinder.Find(id);
                DocumentStorageObject documentStorageObject = getDocumentStorageObject(signStorageObject, file);
                var response = new FileContentResult(documentStorageObject.SignedDocument, "application/octet-stream")
                {
                    FileDownloadName = documentStorageObject.FileName
                };
                return response;
            }
            catch (Exception Error)
            {
                GeneralError = ErrorHelper.GetError(Error);
                return null;
            }
        }

        private DocumentStorageObject getDocumentStorageObject(SignStorageObject signStorageObject, string file)
        {
            foreach (var documentStorageObject in signStorageObject.Documents)
            {
                if (documentStorageObject.FileName.Equals(file))
                {
                    return documentStorageObject;
                }
            }
            throw ErrorHelper.GetException("Download failed", "file argument invalid!");
        }
    }
}