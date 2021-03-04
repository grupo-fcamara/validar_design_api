using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Api
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

        public ApiPathPart[] Identifiers => Parts.Where(p => p.IsIdentifier).ToArray();
        public ApiPathPart[] Resources => Parts.Where(p => p.IsResource).ToArray();
        public ApiPathPart[] Operations => Parts.Where(p => p.IsOperation).ToArray();

        public ApiPathPart Root => Parts.First();
        public ApiPathPart Last => Parts.Last();

        public int Levels => Resources.Count();
        #endregion

        public override string ToString() => raw;

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is ApiPath))
                return false;

            var other = (ApiPath)obj;   
            return Parts.AllEqual(other.Parts);
        }

        public override int GetHashCode()
        {
            return Parts.Sum(p => p.GetHashCode());
        }
    }
}