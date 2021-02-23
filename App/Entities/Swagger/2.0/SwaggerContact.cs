namespace App.Entities.Swagger.Two
{
    public class SwaggerContact : ISwaggerProperty
    {
        public bool IsValid => true;

        // Structure
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
    }
}