namespace App.Entities.Swagger.Two
{
    // Structure
    public partial class SwaggerItems : ISwaggerProperty
    {
        public string Type { get; set; }
        public string Format { get; set; }
        public bool AllowEmptyValue { get; set; }
        public SwaggerItems Items { get; set; }
        public string CollectionFormat { get; set; }
        public dynamic Default { get; set; }
        public float Maximum { get; set; }
        public bool ExclusiveMaximum { get; set; }
        public float Minimum { get; set; }
        public bool ExclusiveMinimum { get; set; }
        public int MaxLength { get; set; }
        public int MinLength { get; set; }
        public string Pattern { get; set; }
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
        public bool UniqueItems { get; set; }
        public dynamic[] Enum { get; set; }
        public float MultipleOf { get; set; }
    }

    //Interface implementation
    public partial class SwaggerItems : ISwaggerProperty
    {
        public virtual bool IsValid =>
            !string.IsNullOrWhiteSpace(Type) &&
            (!Type.Equals("array") || Items != null);
    }
}