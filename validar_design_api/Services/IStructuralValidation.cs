using validar_design_api.Entities;

namespace validar_design_api.Services
{
    public interface IStructuralValidation
    {
        bool Validate(StructuralData data);
    }
}