using Newtonsoft.Json;

namespace SignSupportDemo.SignSupport.API
{
    public class TbsData
    {
        [JsonProperty(PropertyName = "docId", Required = Required.Always)]
        public string DocId { get; set; }

        [JsonProperty(PropertyName = "docType", Required = Required.Always)]
        public string DocType { get; set; }

        [JsonProperty(PropertyName = "tbs", Required = Required.Always)]
        public string Tbs { get; set; }

        [JsonProperty(PropertyName = "signingDate", Required = Required.Always)]
        public long SigningDate { get; set; }
    }
}
