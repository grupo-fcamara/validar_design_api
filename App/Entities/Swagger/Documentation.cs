using System.Collections.Generic;

namespace App.Entities.Swagger
{
    public class Documentation
    {
        public string OpenAPI { get; set; }
        public Dictionary<string, Path> Paths { get; set; }
    }

    public class Path
    {

    }
}