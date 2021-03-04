using System;
using System.Linq;
using App.Entities.Environment;
using App.Entities.Output;
using App.Entities.Swagger;
using App.Services.Validations.Generic;
using App.Services.Validations.Level1;
using App.Services.Validations.Level2;
using App.Services.Validations.Level3;

namespace App.Services.Validations
{
    public class ApiValidator
    {
        private StructuralData _data;

        public ApiValidator(StructuralData data) { _data = data; }

        public ValidationOutput<int> Validate(IDocumentation documentation)
        {
            ILevel[] levels = new ILevel[]
            {
                new ValidationLevel(1,
                    new ValidatePathLevels(_data.PathLevels),
                    new ValidateGetRoutesPerPath(),
                    new ValidateIdentifiers()
                ),

                new ValidationLevel(2,
                    new ValidateRoutesPattern(_data.RoutePattern, _data.Plural),
                    new ValidatePathOperations()
                ),

                new ValidationLevel(3,
                    new ValidatePathHttpVerbs(_data.HttpVerbs),
                    new ValidateStatusCode(_data.StatusCode),
                    new ValidatePagination()
                )
            };

            var output = new ValidationOutput<int>();
            int apiLevel = levels.Max(l => l.Level);

            foreach (var level in levels.OrderBy(l => l.Level))
            {
                var levelOutput = level.Validate(documentation);
                if (!levelOutput.Ok)
                    apiLevel = Math.Min(level.Level - 1, apiLevel);

                output.Concat(levelOutput);
            }

            output.Value = apiLevel;
            return output;
        }
    }
}