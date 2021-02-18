using System.Text.Json.Serialization;

namespace App.Entities.Swagger.Two
{
    //Structure
    public partial class SwaggerSchema : ISwaggerProperty
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
        
        public string Discriminator { get; set; }
        public bool ReadOnly { get; set; }
        public SwaggerXML Xml { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
        public dynamic Example { get; set; }
    }

    //Interface implementation
    public partial class SwaggerSchema : ISwaggerProperty
    {
        public bool IsValid =>
            //Optional
            (Xml == null || Xml.IsValid) &&
            (ExternalDocs == null || ExternalDocs.IsValid);
    }
}