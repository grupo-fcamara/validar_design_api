using App.Entities;
using App.Entities.Swagger;
using App.Services.Validations.Generic;

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

            foreach (var validation in _validations)
            {
                output.Concat(validation.Validate(documentation));
            }
            
            return output;
        }
    }
}
