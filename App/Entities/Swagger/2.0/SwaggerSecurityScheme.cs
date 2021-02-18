using System.Collections.Generic;

namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class SwaggerSecurityScheme : ISwaggerProperty
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string In { get; set; }
        public string Flow { get; set; }
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public Dictionary<string,string> Scopes { get; set; }
    }

    public partial class SwaggerSecurityScheme : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Type) && (
                Type.Equals("basic") || 
                (
                    Type.Equals("apiKey") &&
                    !string.IsNullOrWhiteSpace(Name) && 
                    !string.IsNullOrWhiteSpace(In)
                ) || (
                    Type.Equals("oauth2") &&
                    !string.IsNullOrWhiteSpace(Flow) &&
                    !string.IsNullOrWhiteSpace(AuthorizationUrl)
                )
            );
    }
}