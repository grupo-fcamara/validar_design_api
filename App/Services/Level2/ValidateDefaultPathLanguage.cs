using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Level2
{
    public class ValidateDefaultPathLanguage  : IValidateDefaultPathLanguage
    {
        public string Language { get; protected set; }
        public ValidationOutput Validate(Documentation documentation)
        {
            throw new System.NotImplementedException();
        }
    }
}