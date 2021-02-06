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

        public ApiPath Path { get; set; }
        public ApiPathPart Parent { get; set; }
        public int Index { get; set; }

        public bool IsIdentifier => text.StartsWith('{') && text.EndsWith('}');

        public CASE Case 
        {
            get 
            {
                if (text.All(char.IsLower))
                {
                    if (text.Contains('-'))
                        return CASE.SPINAL;
                    else if (text.Contains('_'))
                        return CASE.SNAKE;
                }                    
                else if (!text.Contains('-') && !text.Contains('_'))
                    return CASE.CAMEL;
                
                return CASE.NOT_SET;
            }
        }

        public override string ToString() => text;
    }
}