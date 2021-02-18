namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class SwaggerTag : ISwaggerProperty
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
    }

    //Interface implementation
    public partial class SwaggerTag : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Name) &&
            //Optional
            (ExternalDocs == null || ExternalDocs.IsValid);
    }
}