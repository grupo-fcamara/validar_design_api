using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Swagger.Two
{
    //Structure
    public partial class SwaggerOperation : ISwaggerProperty
    {
        public string[] Tags { get; set; }
        public string Summary { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
        public string Description { get; set; }
        public string OperationId { get; set; }
        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }
        public SwaggerParameter[] Parameters { get; set; }
        public Dictionary<string, SwaggerResponse> Responses { get; set; }
        public string[] Schemes { get; set; }
        public bool Deprecated { get; set; }
        public Dictionary<string,string[]>[] Security { get; set; }
    }

    //Interface Implementation
    public partial class SwaggerOperation : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            Responses != null && Responses.Values.All(r => r.IsValid) &&
            //Optional
            (ExternalDocs == null || ExternalDocs.IsValid) &&
            (Parameters == null || Parameters.All(p => p.IsValid));
    }
}