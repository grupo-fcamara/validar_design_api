using System.Collections.Generic;

namespace App.Entities 
{
    public enum LANG { NOT_SET = 0, ENGLISH = 1, PORTUGUESE = 2 }
    public enum CASE { NOT_SET = 0, PLURAL, SINGULAR, CAMEL, SNAKE, SPINAL }
    public enum HTTPVERBS { NOT_SET = 0, GET = 1, POST = 2, PUT = 4, DELETE = 8, HEAD = 16, PATCH = 32, OPTIONS = 64 }

    public class StructuralData
    {
        public LANG Language { get; set; }
        public CASE RoutePattern { get; set; }
        public bool Versioned { get; set; }
        public HTTPVERBS[] HttpVerbs { get; set; }
        public Dictionary<string, int[]> StatusCode { get; set; }
        public int PathLevels { get; set; }
        public string BaseUrl { get; set; }
        public string SwaggerPath { get; set; }
    }
}