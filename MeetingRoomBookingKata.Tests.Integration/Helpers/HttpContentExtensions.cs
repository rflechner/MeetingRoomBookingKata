using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeetingRoomBookingKata.Tests.Integration.Helpers
{
    public static class HttpContentExtensions
    {

        public static async Task<T> ReadJsonAsAsync<T>(this HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static StringContent CreateHttpJsonMessage<T>(this T model)
        {
            var message = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");
            
            return message;
        }
    }
}
