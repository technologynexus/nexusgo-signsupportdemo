using SignSupportDemo.Utilities.Multipart;
using System.Collections.Generic;

namespace SignSupportDemo.Utilities.DB
{
    public class DocumentStorageObjectFactory
    {
        public static List<DocumentStorageObject> Create(List<MultipartUploadMedia> uploadMedias)
        {
            List<DocumentStorageObject> documents = new List<DocumentStorageObject>();
            foreach (var UploadMedia in uploadMedias)
            {
                DocumentStorageObject document = new DocumentStorageObject();
                document.FileName = UploadMedia.FileName;
                document.OriginalDocument = UploadMedia.FileData;
                documents.Add(document);
            }
            return documents;
        }
    }
}
