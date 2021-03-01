using App.Entities;
using App.Entities.Swagger;
using App.Services.Validations.Generic;

namespace App.Services.Validations
{
    public class ValidationLevel : ILevel
    {
        private IValidator[] _validations;

        public int Level { get; private set; }
        public ILevel Next { get; set; }

        public ValidationLevel(int level, IValidator[] validations, ILevel next)
        {
            Level = level;
            Next = next;
            _validations = validations;
        }

        public ValidationLevel(int level, IValidator[] validations) : this(level, validations, null) { }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();

            foreach (var validation in _validations)
            {
                output.Concat(validation.Validate(documentation));
            }

            if (Next != null)
                output.Concat(Next.Validate(documentation));
            
            return output;
        }
    }
}
