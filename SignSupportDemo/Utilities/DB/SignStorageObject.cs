using SignSupportDemo.SignSupport.API;
using System;
using System.Collections.Generic;

namespace SignSupportDemo.Utilities.DB
{
    public class SignStorageObject
    {
        public String Id { get; set; }
        public IList<DocumentStorageObject> Documents { get; set; }
        public String SignRequest { get; set; }
        public IList<TbsData> TbsDatas { get; set; }
        public DateTime Expires { get; internal set; }
    }
}
