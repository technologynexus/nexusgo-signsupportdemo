using System;

namespace SignSupportDemo.Utilities.DB
{
    public class DocumentStorageObject
    {
        public String FileName { get; set; }
        public byte[] OriginalDocument { get; set; }
        public byte[] SignedDocument { get; set; }
    }
}
