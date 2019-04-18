using SignSupportDemo.SignSupport.API;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignSupportDemo.Utilities.Form
{
    public class SignFormData
    {
        [Required(ErrorMessage = "Select at least one pdf or xml file")]
        public List<IFormFile> Documents { get; set; }

        [Required]
        public SignRequestGenerationRequest Request { get; set; }
    }
}
