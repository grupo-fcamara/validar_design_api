namespace App.Entities.Swagger.Two
{
    //Structure
    public partial class SwaggerInfo : ISwaggerProperty
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SwaggerContact Contact { get; set; }
        public SwaggerLicense License { get; set; }
        public string Version { get; set; }
    }

    //Interface implementation
    public partial class SwaggerInfo : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Title) &&
            !string.IsNullOrWhiteSpace(Version) &&
            //Optional
            (Contact == null || Contact.IsValid) &&
            (License == null || License.IsValid);
    }
}