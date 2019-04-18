using Newtonsoft.Json;

namespace SignSupportDemo.SignSupport.API
{
    public class SignRequestGenerationResponse
    {
        [JsonProperty(PropertyName = "signRequest", Required = Required.Always)]
        public string SignRequest { get; set; }

        [JsonProperty(PropertyName = "relayState", Required = Required.Always)]
        public string RelayState { get; set; }
    }
}
