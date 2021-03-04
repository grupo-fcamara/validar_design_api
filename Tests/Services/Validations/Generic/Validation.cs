using System.Linq;
using App.Services.Validations.Generic;
using App.Entities.Swagger;
using App.Entities.Output;
using App.Entities.Api;

namespace Tests.Services.Validations
{
    public abstract class Validation
    {
        protected ValidationOutput ReturnProblems(IValidator validator, params string[] paths)
        {
            var doc = new DocumentationForTests();
            doc.Paths = paths.Select(path => new ApiPath(path)).ToArray();

            return validator.Validate(doc);
        }

        protected ValidationOutput ReturnProblems(IValidator validator, params EndPoint[] endPoints)
        {
            var doc = new DocumentationForTests();
            doc.EndPoints = endPoints;

            return validator.Validate(doc);
        }
    }
}