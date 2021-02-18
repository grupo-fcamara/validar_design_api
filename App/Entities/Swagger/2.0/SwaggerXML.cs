namespace App.Entities.Swagger.Two
{
    public class SwaggerXML : ISwaggerProperty
    {
        public bool IsValid => true;

        // Structure
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Prefix { get; set; }
        public bool Attribute { get; set; }
        public bool Wrapped { get; set; }
    }
}