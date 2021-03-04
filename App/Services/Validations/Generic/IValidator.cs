using App.Entities.Output;
using App.Entities.Swagger;

namespace App.Services.Validations.Generic
{
    public interface IValidator
    {
        ValidationOutput Validate(IDocumentation documentation);
    }
}