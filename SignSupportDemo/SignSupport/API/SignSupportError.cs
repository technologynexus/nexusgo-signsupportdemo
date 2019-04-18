using Newtonsoft.Json;

namespace SignSupportDemo.SignSupport.API
{
    public class SignSupportError
    {
        [JsonProperty(PropertyName = "timestamp", Required = Required.Always)]
        public long Timestamp { get; set; }

        [JsonProperty(PropertyName = "status", Required = Required.Always)]
        public int Status { get; set; }

        [JsonProperty(PropertyName = "error", Required = Required.Always)]
        public string Error { get; set; }

        [JsonProperty(PropertyName = "message", Required = Required.Always)]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "path", Required = Required.Always)]
        public string Path { get; set; }

        [JsonProperty(PropertyName = "exception")]
        public string Exception { get; set; }

        // TODO - Enable code below when sign-support supports converting 'errors' to a plain, non-json string.
        // [JsonProperty(PropertyName = "errors")]
        // public string Errors { get; set; }
    }
}
