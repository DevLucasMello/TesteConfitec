using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace TC.Core.Tests.Config
{
    public class DesserializarObjeto
    {
        public async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}
