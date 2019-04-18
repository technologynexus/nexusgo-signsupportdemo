using SignSupportDemo.SignSupport.API;
using SignSupportDemo.Utilities.Json;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace SignSupportDemo.Utilities.Error
{
    public static class ErrorHelper
    {
        public static string GetAllMessages(this Exception exception)
        {
            string message = string.Empty;
            Exception innerException = exception;
            do
            {
                message = message + (string.IsNullOrEmpty(innerException.Message) ? string.Empty : innerException.Message) + " ";
                innerException = innerException.InnerException;
            }
            while (innerException != null);
            return message;
        }

        public static GeneralError GetError(Exception error)
        {
            return new GeneralError
            {
                Message = error.Message,
                DetailedMessage = ErrorHelper.GetAllMessages(error)
            };
        }

        internal static GeneralError GetError(string message)
        {
            return new GeneralError
            {
                Message = message
            };
        }

        internal static bool IsServerError(HttpResponseMessage response)
        {
            return ((int)response.StatusCode >= 500 && (int)response.StatusCode < 600);
        }

        internal static Exception GetServerErrorException(HttpResponseMessage response)
        {
            return new HttpRequestException(response.ReasonPhrase, new HttpRequestException(response.RequestMessage.RequestUri.OriginalString));
        }

        internal static Exception GetSignSupportErrorException(HttpResponseMessage response, byte[] responseBody)
        {
            Console.WriteLine("Response error: {0}", Encoding.UTF8.GetString(responseBody));
            try
            {
                SignSupportError Error = JsonUtil.JsonDeserialize<SignSupportError>(responseBody);
                throw new SignSupportException(Error);
            }
            catch (JsonReaderException e)
            {
                throw new Exception("Error status " + (int)response.StatusCode
                    + " from " + response.RequestMessage.RequestUri.OriginalString, e);
            }
        }
    }
}
