using System.Collections;
using System.Linq;
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

        public ValidationOutput[] Validate(IDocumentation documentation)
        {
            return _validations
                .Select(v => v.Validate(documentation))
                .ToArray();
        }
    }
}
