using SignSupportDemo.Utilities.Error;
using System.Net.Http;
using System.Threading.Tasks;

namespace SignSupportDemo.Utilities.Net
{
    public class NetHelper
    {
        internal static async Task<byte[]> TryReadContentAsync(HttpResponseMessage response)
        {
            if (ErrorHelper.IsServerError(response))
            {
                throw ErrorHelper.GetServerErrorException(response);
            }
            byte[] ResponseBody = await response.Content.ReadAsByteArrayAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw ErrorHelper.GetSignSupportErrorException(response, ResponseBody);
            }
            return ResponseBody;
        }
    }
}
