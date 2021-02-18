using System.Collections.Generic;

namespace App.Entities.Swagger
{
    public interface IDocumentation : ISwaggerProperty
    {
        Dictionary<string, ISwaggerPathItem> GetPaths();
    }
}