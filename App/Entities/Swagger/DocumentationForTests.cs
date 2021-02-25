namespace App.Entities.Swagger
{
    public class DocumentationForTests : IDocumentation
    {
        public string SwaggerVersion { get; set; }
        public EndPoint[] EndPoints { get; set; } = new EndPoint[] { };
        public ApiPath[] Paths { get; set; } = new ApiPath[] { };
        public bool IsValid { get; set; }
    }
}