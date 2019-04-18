using Newtonsoft.Json;
using System.Text;

namespace SignSupportDemo.Utilities.Json
{
    public class JsonUtil
    {
        public static byte[] JsonSerialize<T>(T jsonObject)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(jsonObject));
        }

        public static T JsonDeserialize<T>(byte[] jsonBytes)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(jsonBytes));
        }
    }
}
