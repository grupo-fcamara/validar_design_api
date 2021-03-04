using System.Linq;
using App.Entities.Environment;
using App.Entities.Output;
using App.Entities.Swagger;

namespace App.Services.Validations.Level2
{
    public class ValidateRoutesPattern :  IValidateRoutesPattern
    {
        CasePattern casePattern;
        bool plural;

        public ValidateRoutesPattern(CasePattern casePattern, bool plural)
        {
            this.casePattern = casePattern;
            this.plural = plural;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var paths = documentation.Paths;

            string pluralTxt = plural ? "plural" : "singular";
            foreach (var path in paths)
            {
                if (path.Parts.Any(p => !p.IsRespectingCase(casePattern)))
                    output.AddProblem($"Path {path.ToString()} is not respecting the {casePattern.ToString().ToLower()} case pattern.");

                if (path.Resources.Any(r => (plural && !r.IsPlural) || (!plural && !r.IsSingular)))
                    output.AddProblem($"Path {path.ToString()} is not fully in the {pluralTxt}.");
            }

            return output;
        }
    }
}