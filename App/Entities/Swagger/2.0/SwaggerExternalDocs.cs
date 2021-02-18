namespace App.Entities.Swagger.Two
{
    public class SwaggerExternalDocs : ISwaggerProperty
    {
        public bool IsValid => !string.IsNullOrEmpty(Url);  

        // Structure
        public string Description { get; set; }
        public string Url { get; set; }
    }
}