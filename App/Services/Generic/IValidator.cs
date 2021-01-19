using App.Entities;
using App.Entities.Swagger;

namespace App.Services.Generic
{
    public interface IValidator
    {
        ValidationOutput Validate(Documentation documentation);
    }
}