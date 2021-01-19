using App.Entities.Swagger;

namespace App.Services.Level2
{
    public interface IValidatePathMethods : Generic.IValidator
    {
        string Path { get; }
    }
}