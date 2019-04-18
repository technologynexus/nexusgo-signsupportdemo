using Newtonsoft.Json;
using System.Collections.Generic;

namespace SignSupportDemo.SignSupport.API
{
    public class SignCompletionRequest
    {
        [JsonProperty(PropertyName = "eidSignResponse", Required = Required.Always)]
        public string EidSignResponse { get; set; }

        [JsonProperty(PropertyName = "relayState", Required = Required.Always)]
        public string RelayState { get; set; }

        [JsonProperty(PropertyName = "tbsDatas", Required = Required.Always)]
        public IList<TbsData> TbsDatas { get; set; }
    }
}
