using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Swagger
{
    public interface ISwaggerProperty
    {
        bool IsValid { get; }
    }

    public class Documentation : IDocumentation, ISwaggerProperty
    {
        public bool IsValid => 
            //Required
            !string.IsNullOrWhiteSpace(Swagger) &&
            Info != null && Info.IsValid &&
            Paths != null && Paths.Values.All(p => p.IsValid) &&
            //Optional
            (Definitions == null || Definitions.Values.All(d => d.IsValid)) &&
            (Parameters == null || Parameters.Values.All(p => p.IsValid)) &&
            (Responses == null || Responses.Values.All(r => r.IsValid)) &&
            (SecurityDefinitions == null || SecurityDefinitions.Values.All(d => d.IsValid)) &&
            (ExternalDocs == null || ExternalDocs.IsValid);

        // Structure
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
    public class SwaggerInfo : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Title) &&
            !string.IsNullOrWhiteSpace(Version) &&
            //Optional
            (Contact == null || Contact.IsValid) &&
            (License == null || License.IsValid);

        // Structure
        public string Title { get; set; }
        public string Description { get; set; }
        public SwaggerContact Contact { get; set; }
        public SwaggerLicense License { get; set; }
        public string Version { get; set; }
    }

    public class SwaggerContact : ISwaggerProperty
    {
        public bool IsValid => true;

        // Structure
        public string Name { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
    }

    public class SwaggerLicense : ISwaggerProperty
    {
        public bool IsValid => !string.IsNullOrWhiteSpace(Name);

        // Structure
        public string Name { get; set; }
        public string Url { get; set; }
    }
    #endregion

    #region Path
    public class SwaggerPathItem : ISwaggerProperty
    {
        public bool IsValid =>
            //Optional
            (Get == null || Get.IsValid) &&
            (Put == null || Put.IsValid) &&
            (Post == null || Post.IsValid) &&
            (Delete == null || Delete.IsValid) &&
            (Options == null || Options.IsValid) &&
            (Head == null || Head.IsValid) &&
            (Patch == null || Patch.IsValid) &&
            (Parameters == null || Parameters.All(p => p.IsValid));

        // Structure
        public SwaggerOperation Get { get; set; }
        public SwaggerOperation Put { get; set; }
        public SwaggerOperation Post { get; set; }
        public SwaggerOperation Delete { get; set; }
        public SwaggerOperation Options { get; set; }
        public SwaggerOperation Head { get; set; }
        public SwaggerOperation Patch { get; set; }

        public SwaggerParameter[] Parameters { get; set; }
    }

    public class SwaggerOperation : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            Responses != null && Responses.Values.All(r => r.IsValid) &&
            //Optional
            (ExternalDocs == null || ExternalDocs.IsValid) &&
            (Parameters == null || Parameters.All(p => p.IsValid));

        // Structure
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
    public class SwaggerSchema : ISwaggerProperty
    {
        public bool IsValid =>
            //Optional
            (Xml == null || Xml.IsValid) &&
            (ExternalDocs == null || ExternalDocs.IsValid);

        // Structure
        public string Discriminator { get; set; }
        public bool ReadOnly { get; set; }
        public SwaggerXML Xml { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
        public dynamic Example { get; set; }
    }

    public class SwaggerXML : ISwaggerProperty
    {
        public bool IsValid => true;

        // Structure
        public string Name { get; set; }
        public string Namespace { get; set; }
        public string Prefix { get; set; }
        public bool Attribute { get; set; }
        public bool Wrapped { get; set; }
    }
    #endregion

    public class SwaggerParameter : SwaggerItems, ISwaggerProperty
    {
        public override bool IsValid =>
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(In) &&
            In.Equals("body") ? (Schema != null && Schema.IsValid) : base.IsValid;

        // Structure
        public string Name { get; set; }
        public string In { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }

        public SwaggerSchema Schema { get; set; }
        public bool AllowEmptyValues { get; set; }
    }
    
    #region Response
    public class SwaggerResponse : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Description) &&
            //Optional
            (Schema == null || Schema.IsValid) &&
            (Headers == null || Headers.Values.All(h => h.IsValid));

        // Structure
        public string Description { get; set; }
        public SwaggerSchema Schema { get; set; }
        public Dictionary<string, SwaggerHeader> Headers { get; set; }
        public Dictionary<string, Dictionary<string, string>> Example { get; set; }
    }

    public class SwaggerHeader : SwaggerItems, ISwaggerProperty
    {
        public string Description { get; set; }
    }
    #endregion

    public class SwaggerItems : ISwaggerProperty
    {
        public virtual bool IsValid =>
            !string.IsNullOrWhiteSpace(Type) &&
            (!Type.Equals("array") || Items != null);

        // Structure
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

    public class SwaggerTag : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Name) &&
            //Optional
            (ExternalDocs == null || ExternalDocs.IsValid);

        // Structure
        public string Name { get; set; }
        public string Description { get; set; }
        public SwaggerExternalDocs ExternalDocs { get; set; }
    }

    public class SwaggerExternalDocs : ISwaggerProperty
    {
        public bool IsValid => !string.IsNullOrEmpty(Url);  

        // Structure
        public string Description { get; set; }
        public string Url { get; set; }
    }

    public class SwaggerSecurityScheme : ISwaggerProperty
    {
        public bool IsValid =>
            //Required
            !string.IsNullOrWhiteSpace(Type) &&
            !string.IsNullOrWhiteSpace(Description) &&
            !string.IsNullOrWhiteSpace(Name) &&
            !string.IsNullOrWhiteSpace(In) &&
            !string.IsNullOrWhiteSpace(Flow) &&
            !string.IsNullOrWhiteSpace(AuthorizationUrl) &&
            !string.IsNullOrWhiteSpace(TokenUrl);

        // Structure
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