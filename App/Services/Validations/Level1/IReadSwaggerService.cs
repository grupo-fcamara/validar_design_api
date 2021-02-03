using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Validations.Level1
{
    public interface IReadSwaggerService
    {
        Documentation Get(StructuralData data);
    }
}