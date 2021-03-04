using App.Entities.Environment;

namespace App.Services.Environment
{
    public interface IGetEnvironmentVariables
    {
        StructuralData GetData();
    }
}