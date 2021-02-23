namespace App.Entities.Swagger
{
    public class DocumentationForTests : IDocumentation
    {
        public string SwaggerVersion { get; set; }
        public EndPoint[] EndPoints { get; set; }
        public ApiPath[] Paths { get; set; }
        public bool IsValid { get; set; }
    }
}