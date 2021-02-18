using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Swagger.Two
{
    //Structure
    public partial class SwaggerPathItem : ISwaggerPathItem
    {
        public SwaggerOperation Get { get; set; }
        public SwaggerOperation Put { get; set; }
        public SwaggerOperation Post { get; set; }
        public SwaggerOperation Delete { get; set; }
        public SwaggerOperation Options { get; set; }
        public SwaggerOperation Head { get; set; }
        public SwaggerOperation Patch { get; set; }

        public SwaggerParameter[] Parameters { get; set; }
    }

    //Interface Implementation
    public partial class SwaggerPathItem : ISwaggerPathItem
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

        public Dictionary<HTTPVERBS, ISwaggerOperation> GetOperations()
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
            }
            .ToDictionary(pair => pair.Key, pair => pair.Value as ISwaggerOperation);
        }
    }
}