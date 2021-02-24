using System;
using App.Services;
using Humanizer;

namespace App.Entities
{
    public class ApiPathPart
    {
        string text;

        public ApiPathPart(string text, ApiPath path, ApiPathPart parent, int index)
        { 
            this.text = text;

            Path = path;
            Parent = parent;
            Index = index;
        }

        #region Properties
        public ApiPath Path { get; set; }
        public ApiPathPart Parent { get; set; }
        public int Index { get; set; }

        //Need to implement differentiation between resource and operation
        public bool IsIdentifier => text.StartsWith('{') && text.EndsWith('}');
        public bool IsOperation => !IsIdentifier && OperationCheckerService.IsOperation(text);
        public bool IsResource => !IsIdentifier && !IsOperation;

        public bool IsPlural => text.Equals(text.Pluralize(false));
        public bool IsSingular => text.Equals(text.Singularize(false));

        public bool IsRespectingCase(CASE casePattern)
        {
            switch (casePattern)
            {
                case CASE.CAMEL:
                    string aux = text.Replace("-", "").Replace("_", "");
                    return text.Equals(aux.Camelize()) || text.Equals(aux.Pascalize());
                case CASE.SNAKE:
                    return text.Equals(text.Underscore());
                case CASE.SPINAL:
                    return text.Equals(text.Kebaberize());
                default:
                    return true;
            }
        }
        #endregion

        public override string ToString() => text;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ApiPathPart))
                return false;

            var other = (ApiPathPart)obj;   
            return text.Equals(other.text);
        }

        public override int GetHashCode()
        {
            return text.GetHashCode();
        }
    }
}