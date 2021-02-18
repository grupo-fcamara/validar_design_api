using System.Linq;
using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level2
{
    public class ValidateRoutesPattern :  IValidateRoutesPattern
    {
        CASE casePattern;
        bool plural;

        public ValidateRoutesPattern(CASE casePattern, bool plural)
        {
            this.casePattern = casePattern;
            this.plural = plural;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            //Getting paths from document
            var rawPaths = documentation.GetPaths().Keys;
            var paths = rawPaths.Select(s => new ApiPath(s));

            string pluralTxt = plural ? "plural" : "singular";
            foreach (var path in paths)
            {
                if (path.Parts.Count(p => !p.IsRespectingCase(casePattern)) > 0)
                    output.AddProblem($"Path {path.ToString()} is not respecting the {casePattern.ToString().ToLower()} case pattern.");

                if (path.Resources.Count(r => (plural && !r.IsPlural) || (!plural && !r.IsSingular)) > 0)
                    output.AddProblem($"Path {path.ToString()} is not fully in the {pluralTxt}.");
            }

            return output;
        }
    }
}