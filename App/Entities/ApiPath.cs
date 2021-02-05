using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Entities
{
    public class ApiPath
    {
        string raw;

        public string[] Pieces => raw.Split('/').Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        public Dictionary<int, string> Identifiers
        {
            get
            {
                var dic = new Dictionary<int, string>();
                for (int i = 0; i < Pieces.Length; i++)
                {
                    if (IsIdentifier(Pieces[i]))
                        dic[i] = Pieces[i];
                }

                return dic;
            }
        }

        public int Levels => Pieces.Count(p => !IsIdentifier(p));
        public int IdentifiersCount => Pieces.Count(IsIdentifier);

        public ApiPath(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Invalid path.");
            
            raw = path; 
        }

        public static bool IsIdentifier(string piece)
        {
            return piece.StartsWith('{') && piece.EndsWith('}');
        }

        public override string ToString() => raw;
    }
}