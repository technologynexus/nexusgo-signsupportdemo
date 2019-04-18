using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace SignSupportDemo.SignSupport.API
{
    public class SignResponseData
    {
        [Required]
        public string EidSignResponse { get; set; }

        [Required]
        public string RelayState { get; set; }

    }
}
