namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class SwaggerParameter : SwaggerItems, ISwaggerProperty
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        public SwaggerSchema Schema { get; set; }
        public bool AllowEmptyValues { get; set; }
    }

    //Interface implementation
    public partial class SwaggerParameter : SwaggerItems, ISwaggerProperty
    {
        public override bool IsValid =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(In) &&
            In.Equals("body") ? (Schema != null && Schema.IsValid) : base.IsValid;
    }
}