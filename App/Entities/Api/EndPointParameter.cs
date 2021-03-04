using System;
using Humanizer;

namespace App.Entities.Api
{   
    public enum ParameterLocalization { QUERY, HEADER, PATH, FORM_DATA, BODY }

    public class EndPointParameter
    {
        public string Name { get; set; }
        public ParameterLocalization Local { get; set; }

        public EndPointParameter(string name, ParameterLocalization local)
        {
            Name = name;
            Local = local;
        }

        public EndPointParameter(string name, string local)
            : this(name, Enum.Parse<ParameterLocalization>(local.Underscore(), true)) { }
    }
}