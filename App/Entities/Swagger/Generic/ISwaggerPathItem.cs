using System.Collections.Generic;
using App.Entities.Swagger.Two;

namespace App.Entities.Swagger
{
    public interface ISwaggerPathItem : ISwaggerProperty
    {
        Dictionary<HTTPVERBS, ISwaggerOperation> GetOperations();
        HTTPVERBS[] GetVerbs();
    }
}