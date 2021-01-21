using System.Collections.Generic;

namespace App.Entities.Swagger
{
    public class Documentation : IDocumentation
    {
        public string Swagger { get; set; }
        public Info Info { get; set; }

        public string Host { get; set; }
        public string BasePath { get; set; }
        public string[] Schemes { get; set; }

        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }

        public Dictionary<string, Dictionary<string, Operation>> Paths { get; set; }
    }

    public class Info
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }

    public class Operation
    {
        public string Summary { get; set; }
        public string Description { get; set; }
        public string[] Produces { get; set; }
        public string[] Consumes { get; set; }
        public Dictionary<int, Response> Responses { get; set; }
        public bool Deprecated { get; set; }
    }

    public class Response
    {

    }
}