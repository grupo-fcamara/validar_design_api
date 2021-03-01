using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Generic
{
    public interface IValidator
    {
        ValidationOutput Validate(IDocumentation documentation);
    }
    
    public interface IValidator<T>
    {
        IValidationOutput<T> Validate(IDocumentation documentation);
    }
}