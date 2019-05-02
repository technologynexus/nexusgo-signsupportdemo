using SignSupportDemo.SignSupport.API;

namespace SignSupportDemo.Utilities.Form
{
    public class SignFormDataFactory
    {
        public static SignFormData Create()
        {
            return new SignFormData
            {
                Request = new SignRequestGenerationRequest
                {
                    Idp = "https://dss.nexusville.com/idp",
                    Signer = "",
                    SignMessage = new SignMessage
                    {
                        Message = "<b>You are requested to sign the following:</b><br>Accept purchase agreement",
                        MessageFormat = "text/html",
                        MustShow = true,
                        DoEncrypt = true
                    }
                }
            };
        }
    }
}
