using App.Entities.Api;

namespace App.Entities.Swagger
{
    public interface IDocumentation : ISwaggerProperty
    {
        string SwaggerVersion { get; }
        EndPoint[] EndPoints { get; }
        ApiPath[] Paths { get; }
    }
}