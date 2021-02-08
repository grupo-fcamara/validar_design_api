using System.Linq;
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

        public bool IsIdentifier => text.StartsWith('{') && text.EndsWith('}');

        public bool IsRespectingCase(CASE casePattern)
        {
            text = text.Replace("{", "").Replace("}", "");

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
    }
}