using System.Collections;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Services.Validations.Generic;
using Humanizer;

namespace App.Services.Validations
{
    public class ValidationLevel : ILevel
    {
        private IValidator[] _validations;

        public int Level { get; private set; }

        public ValidationLevel(int level, params IValidator[] validations)
        {
            Level = level;
            _validations = validations;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            output.AddMessage($"Checking level {Level}...");
            
            foreach (var validation in _validations)
            {
                output.Concat(validation.Validate(documentation));
            }

            if (output.Ok)
                output.AddMessage($"No problems found in level {Level}.");
            else
                output.AddMessage($"Found {"problem".ToQuantity(output.Problems.Count())} in level {Level}.");

            return output;
        }
    }
}
