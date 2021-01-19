using System.Net;
using System.Text.Json;
using App.Entities.Swagger;

namespace App.Services
{
    public class SwaggerJsonDeserializer
    {
        public Documentation GetByUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                return Deserialize(json);
            }
        }

        private Documentation Deserialize(string json)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<Documentation>(json, options);
        }
    }
}