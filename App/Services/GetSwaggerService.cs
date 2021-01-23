using System.IO;
using System.Net;
using System.Text.Json;
using App.Entities.Swagger;

namespace App.Services
{
    public class GetSwaggerService
    {
        public Documentation GetByUrl(string url)
        {
            using (WebClient wc = new WebClient())
            {
                var json = wc.DownloadString(url);
                return Deserialize(json);
            }
        }

        public Documentation Deserialize(string json)
        {
            var options = new JsonSerializerOptions();
            options.PropertyNameCaseInsensitive = true;
            return JsonSerializer.Deserialize<Documentation>(json, options);
        }
    }
}