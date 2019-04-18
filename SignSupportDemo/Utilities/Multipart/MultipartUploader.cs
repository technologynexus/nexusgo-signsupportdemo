using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SignSupportDemo.Utilities.Multipart
{
    public class MultipartUploader
    {
        public static async Task<HttpResponseMessage> Upload(String requestUrl, IList<MultipartUploadMedia> uploadMedias)
        {
            return await MultipartUploader.Upload(requestUrl, uploadMedias, null);
        }

        public static async Task<HttpResponseMessage> Upload(String requestUrl, IList<MultipartUploadMedia> uploadMedias, string requestJson)
        {
            MultipartFormDataContent MultipartContent = new MultipartFormDataContent();
            foreach (var UploadMedia in uploadMedias)
            {
                MultipartContent.Add(new StreamContent(UploadMedia.FileStream()), UploadMedia.PartName, UploadMedia.FileName);
            }
            if (requestJson != null)
            {
                StringContent JsonContent = new StringContent(requestJson, Encoding.UTF8, "application/json");
                MultipartContent.Add(JsonContent, "request");
            }

            HttpRequestMessage Request = new HttpRequestMessage
            {
                RequestUri = new Uri(requestUrl),
                Method = HttpMethod.Post,
                Content = MultipartContent
            };

            using (HttpClient HttpClient = new HttpClient())
            {
                return await HttpClient.SendAsync(Request).ConfigureAwait(false);
            }
        }
    }
}
