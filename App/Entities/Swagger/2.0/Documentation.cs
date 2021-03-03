using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class Documentation : IDocumentation
    {
        public string Swagger { get; set; }
        public SwaggerInfo Info { get; set; }

        public string Host { get; set; }
        public string BasePath { get; set; }
        public string[] Schemes { get; set; }

        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }

        public Dictionary<string, SwaggerPathItem> Paths { get; set; }
        public Dictionary<string, SwaggerSchema> Definitions { get; set; }
        public Dictionary<string, SwaggerParameter> Parameters { get; set; }
        public Dictionary<string, SwaggerResponse> Responses { get; set; }
        public Dictionary<string, SwaggerSecurityScheme> SecurityDefinitions { get; set; }
        public Dictionary<string, string[]>[] Security { get; set; }

        public SwaggerExternalDocs ExternalDocs { get; set; }

        public Documentation()
        {
            Paths = new Dictionary<string, SwaggerPathItem>();
            Definitions = new Dictionary<string, SwaggerSchema>();
            Parameters = new Dictionary<string, SwaggerParameter>();
            Responses = new Dictionary<string, SwaggerResponse>();
            SecurityDefinitions = new Dictionary<string, SwaggerSecurityScheme>();
            Security = new Dictionary<string, string[]>[] { };
        }
    }

    // Interface implementation
    public partial class Documentation : IDocumentation
    {
        public bool IsValid => 
            //Required
            !string.IsNullOrWhiteSpace(Swagger) &&
            Info != null && Info.IsValid &&
            Paths != null && Paths.Values.All(p => p.IsValid) &&
            //Optional
            Definitions.Values.All(d => d.IsValid) &&
            Parameters.Values.All(p => p.IsValid) &&
            Responses.Values.All(r => r.IsValid) &&
            SecurityDefinitions.Values.All(d => d.IsValid) &&
            (ExternalDocs == null || ExternalDocs.IsValid);

        public string SwaggerVersion => Swagger;

        public EndPoint[] EndPoints
        {
            get
            {
                var list = new List<EndPoint>();
                foreach (var pair in Paths)
                {
                    var operations = pair.Value.GetOperations();
                    foreach (var operation in operations.Where(pair => pair.Value != null))
                    {
                        var endPoint = new EndPoint(new ApiPath(pair.Key), operation.Key);
                        endPoint.Responses = operation.Value.Responses.Keys.Select(key => int.Parse(key)).ToArray();
                        endPoint.Parameters = operation.Value.Parameters.Cast<ISwaggerParameter>().ToArray();
                        
                        list.Add(endPoint);
                    }
                }

                return list.ToArray();
            }
        }

        ApiPath[] IDocumentation.Paths => 
            EndPoints
            .Select(e => e.Path)
            .Distinct(p => p.ToString())
            .ToArray();
    }
}