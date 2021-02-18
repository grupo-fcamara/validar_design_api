namespace App.Entities.Swagger.Two
{
    public class SwaggerLicense : ISwaggerProperty
    {
        public bool IsValid => !string.IsNullOrWhiteSpace(Name);

        // Structure
        public string Name { get; set; }
        public string Url { get; set; }
    }
}