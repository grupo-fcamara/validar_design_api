using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace App.Entities.Swagger.Two
{
    //Structure
    public partial class SwaggerPathItem
    {
        [JsonPropertyName("$ref")]
        public string Ref { get; set; }
        
        public SwaggerOperation Get { get; set; }
        public SwaggerOperation Put { get; set; }
        public SwaggerOperation Post { get; set; }
        public SwaggerOperation Delete { get; set; }
        public SwaggerOperation Options { get; set; }
        public SwaggerOperation Head { get; set; }
        public SwaggerOperation Patch { get; set; }

        public SwaggerParameter[] Parameters { get; set; }
    }

    //Interface Implementation and Auxiliary Methods
    public partial class SwaggerPathItem
    {
        public bool IsValid =>
            //Optional
            GetOperations()
                .Where(pair => pair.Value != null)
                .All(pair => pair.Value.IsValid) &&
            (Parameters == null || Parameters.All(p => p.IsValid));

        public HttpVerbs[] GetVerbs()
        {
            return GetOperations()
                .Where(pair => pair.Value != null)
                .Select(pair => pair.Key)
                .ToArray();
        }          

        public Dictionary<HttpVerbs, SwaggerOperation> GetOperations()
        {
            return new Dictionary<HttpVerbs, SwaggerOperation>()
            {
                { HttpVerbs.GET, Get },
                { HttpVerbs.POST, Post },
                { HttpVerbs.PUT, Put },
                { HttpVerbs.DELETE, Delete },
                { HttpVerbs.HEAD, Head },
                { HttpVerbs.PATCH, Patch },
                { HttpVerbs.OPTIONS, Options }
            };
        }
    }
}