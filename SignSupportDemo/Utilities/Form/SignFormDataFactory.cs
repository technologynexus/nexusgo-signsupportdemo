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
                    Idp = "https://test-auth.nexusgroup.com/idp",
                    Signer = "188803099368",
                    SignMessage = new SignMessage
                    {
                        Message = "<b>You are requested to sign the following:</b><br>\nAccept purchase agreement",
                        MessageFormat = "text/html",
                        MustShow = true,
                        DoEncrypt = false
                    }
                }
            };
        }
    }
}
