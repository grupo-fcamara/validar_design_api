using System.Collections.Generic;

namespace App.Entities.Swagger
{
    public class Documentation : IDocumentation
    {
        public string Swagger { get; set; }
        public SwaggerInfo Info { get; set; }

        public string Host { get; set; }
        public string BasePath { get; set; }
        public string[] Schemes { get; set; }

        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }

        public Dictionary<string, SwaggerPathItem> Paths { get; set; }
        public Dictionary<string, SwaggerSchema> Definitions { get; set; }
        public Dictionary<string, SwaggerParameter> Parameters { get; set; }
        public Dictionary<string, SwaggerResponse> Responses { get; set; }
        public Dictionary<string, SwaggerSecurityScheme> SecurityDefinitions { get; set; }
        public Dictionary<string, string[]> Security { get; set; }

        public SwaggerExternalDocs ExternalDocs { get; set; }
    }

    #region Info
    public class SwaggerInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public SwaggerContact Contact { get; set; }
        public SwaggerLicense License { get; set; }
        public string Version { get; set; }
    }

    public class SwaggerContact
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
    }

    public class SwaggerLicense
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
    #endregion

    #region Path
    public class SwaggerPathItem
    {
        public SwaggerOperation Get { get; set; }
        public SwaggerOperation Put { get; set; }
        public SwaggerOperation Post { get; set; }
        public SwaggerOperation Delete { get; set; }
        public SwaggerOperation Options { get; set; }
        public SwaggerOperation Head { get; set; }
        public SwaggerOperation Patch { get; set; }

        public SwaggerParameter[] Parameters { get; set; }
    }

    public class SwaggerOperation
    {
        public string[] Tags { get; set; }
        public string Summary { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
        public string Description { get; set; }
        public string OperationId { get; set; }
        public string[] Consumes { get; set; }
        public string[] Produces { get; set; }
        public SwaggerParameter[] Parameters { get; set; }
        public Dictionary<string, SwaggerResponse> Responses { get; set; }
        public string[] Schemes { get; set; }
        public bool Deprecated { get; set; }
        public Dictionary<string,string[]> Security { get; set; }
    }
    #endregion

    #region Schema
    public class SwaggerSchema
    {
        public string Discriminator { get; set; }
        public bool ReadOnly { get; set; }
        public SwaggerXML Xml { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
        public dynamic Example { get; set; }
    }

    public class SwaggerXML
    {
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Prefix { get; set; }
        public bool Attribute { get; set; }
        public bool Wrapped { get; set; }
    }
    #endregion

    public class SwaggerParameter : SwaggerItems
    {
        public string Name { get; set; }
        public string In { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        //if In == "body"
        public SwaggerSchema Schema { get; set; }
    }
    
    #region Response
    public class SwaggerResponse
    {
        public string Description { get; set; }
        public SwaggerSchema Schema { get; set; }
        public Dictionary<string, SwaggerHeader> Headers { get; set; }
        public Dictionary<string, Dictionary<string, string>> Example { get; set; }
    }

    public class SwaggerHeader : SwaggerItems
    {
        public string Description { get; set; }
    }
    #endregion

    public class SwaggerItems
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

    public class Tag
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
    }

    public class SwaggerExternalDocs
    {
        public string Url { get; set; }
        public string Description { get; set; }
    }

    public class SwaggerSecurityScheme
    {
        public string Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string In { get; set; }
        public string Flow { get; set; }
        public string AuthorizationUrl { get; set; }
        public string TokenUrl { get; set; }
        public Dictionary<string,string> Scopes { get; set; }
    }
}