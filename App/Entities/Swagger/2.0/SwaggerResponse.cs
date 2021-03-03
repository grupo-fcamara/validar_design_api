using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class SwaggerResponse : ISwaggerProperty
    {
        public string Description { get; set; }
        public SwaggerSchema Schema { get; set; }
        public Dictionary<string, SwaggerHeader> Headers { get; set; }
        public Dictionary<string, Dictionary<string, string>> Example { get; set; }

        public SwaggerResponse()
        {
            Headers = new Dictionary<string, SwaggerHeader>();
            Example = new Dictionary<string, Dictionary<string, string>>() { };
        }
    }

    public partial class SwaggerResponse : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Description) &&
            //Optional
            (Schema == null || Schema.IsValid) &&
            Headers.Values.All(h => h.IsValid);
    }
}