using System;
using System.IO;
using System.Net;
using System.Text.Json;
using App.Entities.Swagger;
using App.Entities.Swagger.Two;

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

            var doc = JsonSerializer.Deserialize<Documentation>(json, options);

            if (!doc.IsValid)
                throw new Exception("Invalid documentation.");

            return doc;
        }
    }
}