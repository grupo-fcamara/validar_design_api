using System.Linq;
using System.Collections.Generic;
using App.Entities.Swagger.Two;
using App.Services.Validations.Generic;
using App.Entities;

namespace Tests
{
    public abstract class ValidatePaths
    {
        protected ValidationOutput ReturnProblems(IValidator validator, params string[] paths)
        {
            var doc = new Documentation();
            doc.Paths = new Dictionary<string, SwaggerPathItem>();
            var pathItem = new SwaggerPathItem { Get = new SwaggerOperation() };
            
            paths.ToList().ForEach(path => doc.Paths[path] = pathItem);
            return validator.Validate(doc);
        }
    }
}