using System.Linq;
using App.Services.Validations.Generic;
using App.Entities;
using App.Entities.Swagger;
using System.Collections.Generic;

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

        protected EndPoint[] GetMultipleEndPoints(string path, params HTTPVERBS[] verbs)
        {
            var endPoints = new List<EndPoint>();
            foreach (var verb in verbs)
            {
                endPoints.Add(new EndPoint() { Path = new ApiPath(path), Verb = verb });
            }
            return endPoints.ToArray();
            
        }

        protected EndPoint[] GetMultipleEndPoints(HTTPVERBS verb, params string[] paths)
        {
            var endPoints = new List<EndPoint>();
            foreach (var path in paths)
            {
                endPoints.Add(new EndPoint() { Path = new ApiPath(path), Verb = verb });
            }
            return endPoints.ToArray();
        }
    }
}