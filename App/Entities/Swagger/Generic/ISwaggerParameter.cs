namespace App.Entities.Swagger
{   
    public enum ParameterLocalization { QUERY, HEADER, PATH, FORM_DATA, BODY }

    public interface ISwaggerParameter
    {
        string Name { get; }
        ParameterLocalization In { get; }
    }
}