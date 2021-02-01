using App.Entities;

namespace App.Services
{
    public interface IStructuralValidation
    {
        bool Validate(StructuralData data);
    }
}