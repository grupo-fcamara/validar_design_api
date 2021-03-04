using System.Collections.Generic;
using System.Linq;
using App.Entities.Api;

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
                    var verbOperations = pair.Value.GetOperations();
                    foreach (var verbOperationPair in verbOperations.Where(pair => pair.Value != null))
                    {
                        var verb = verbOperationPair.Key;
                        var operation = verbOperationPair.Value;

                        var endPoint = new EndPoint(new ApiPath(pair.Key), verb);
                        endPoint.Responses = operation.Responses.Keys.Select(key => int.Parse(key)).ToArray();
                        endPoint.Parameters = operation.Parameters.Select(p => new EndPointParameter(p.Name, p.In)).ToArray();
                        
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