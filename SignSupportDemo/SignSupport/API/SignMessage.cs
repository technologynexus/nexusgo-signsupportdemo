using Newtonsoft.Json;
using System;

namespace SignSupportDemo.SignSupport.API
{
    public class SignMessage
    {
        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "messageFormat", Required = Required.Always)]
        public string MessageFormat { get; set; }

        [JsonProperty(PropertyName = "mustShow", Required = Required.Always)]
        public Boolean MustShow { get; set; }

        [JsonProperty(PropertyName = "doEncrypt", Required = Required.Always)]
        public Boolean DoEncrypt { get; set; }
    }
}
