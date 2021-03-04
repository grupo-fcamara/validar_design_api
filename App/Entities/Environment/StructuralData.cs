namespace App.Entities.Environment
{
    public enum Language { ENGLISH, PORTUGUESE }
    public enum CasePattern { CAMEL, SNAKE, SPINAL }
    public enum HttpVerbs { GET = 1, POST = 2, PUT = 4, DELETE = 8, HEAD = 16, PATCH = 32, OPTIONS = 64 }

    public class StructuralData
    {
        public Language Language { get; set; }
        public CasePattern RoutePattern { get; set; }
        public bool Plural { get; set; }
        public bool Versioned { get; set; }
        public HttpVerbs[] HttpVerbs { get; set; }
        public StatusCodePerVerb StatusCode { get; set; }
        public int PathLevels { get; set; }
        public string BaseUrl { get; set; }
        public string SwaggerPath { get; set; }
    }
}