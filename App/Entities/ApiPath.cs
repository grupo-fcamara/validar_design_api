using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Entities
{
    public class ApiPath
    {
        string raw;

        public ApiPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Invalid path.");
            
            raw = path; 
        }

        #region Properties
        public ApiPathPart[] Parts
        {
            get
            {
                var rawParts = raw.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
                var parts = new List<ApiPathPart>();

                for (int i = 0; i < rawParts.Length; i++)
                {
                    var parent = i > 0 ? parts[i - 1] : null;
                    parts.Add(new ApiPathPart(rawParts[i], this, parent, i));
                }

                return parts.ToArray();
            }
        }

        public int Levels => Parts.Count(p => !p.IsIdentifier);

        public ApiPathPart[] Identifiers => Parts.Where(p => p.IsIdentifier).ToArray();
        #endregion

        public override string ToString() => raw;
    }

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

            if (text.All(char.IsLower))
                return true;

            switch (casePattern)
            {
                case CASE.CAMEL:
                    return text.All(c => char.IsLetter(c) || char.IsNumber(c));
                case CASE.SNAKE:
                    return text.Contains('_') && !text.Contains('-') && !text.Any(c => char.IsUpper(c));
                case CASE.SPINAL:
                    return text.Contains('-') && !text.Contains('_') && !text.Any(c => char.IsUpper(c));
                default:
                    return true;
            }
        }
        #endregion

        public override string ToString() => text;
    }
}