using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignSupportDemo.SignSupport.API
{
    public class SignRequestGenerationRequest
    {
        [JsonProperty(PropertyName = "signer", NullValueHandling = NullValueHandling.Ignore)]
        public string Signer { get; set; }

        [Required]
        [JsonProperty(PropertyName = "idp", Required = Required.Always)]
        public string Idp { get; set; }

        [JsonProperty(PropertyName = "loa", NullValueHandling = NullValueHandling.Ignore)]
        public string Loa { get; set; }

        [JsonProperty(PropertyName = "tbsDatas", Required = Required.Always)]
        public List<TbsData> TbsDatas { set; get; }

        [JsonProperty(PropertyName = "signMessage", NullValueHandling = NullValueHandling.Ignore)]
        public SignMessage SignMessage { set; get; }

        [JsonProperty(PropertyName = "encryptSignMessage", NullValueHandling = NullValueHandling.Ignore)]
        public Boolean EncryptSignMessage { set; get; }
    }
}
