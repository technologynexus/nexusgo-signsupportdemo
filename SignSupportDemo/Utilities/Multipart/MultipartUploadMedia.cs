using System.IO;

namespace SignSupportDemo.Utilities.Multipart
{
    public class MultipartUploadMedia
    {
        public byte[] FileData { get; }
        public string PartName { get; }
        public string FileName { get; }

        public MultipartUploadMedia(byte[] fileData, string partName, string fileName)
        {
            this.FileData = fileData;
            this.PartName = partName;
            this.FileName = fileName;
        }

        public Stream FileStream()
        {
            return new MemoryStream(FileData);
        }
    }
}
