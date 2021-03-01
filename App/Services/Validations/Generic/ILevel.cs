using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Generic
{
    public interface ILevel
    {
        int Level { get; }
        ValidationOutput[] Validate(IDocumentation documentation);
    }
}