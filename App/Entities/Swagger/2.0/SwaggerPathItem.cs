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

        public HTTPVERBS[] GetVerbs()
        {
            return GetOperations()
                .Where(pair => pair.Value != null)
                .Select(pair => pair.Key)
                .ToArray();
        }          

        public Dictionary<HTTPVERBS, SwaggerOperation> GetOperations()
        {
            return new Dictionary<HTTPVERBS, SwaggerOperation>()
            {
                { HTTPVERBS.GET, Get },
                { HTTPVERBS.POST, Post },
                { HTTPVERBS.PUT, Put },
                { HTTPVERBS.DELETE, Delete },
                { HTTPVERBS.HEAD, Head },
                { HTTPVERBS.PATCH, Patch },
                { HTTPVERBS.OPTIONS, Options }
            };
        }
    }
}