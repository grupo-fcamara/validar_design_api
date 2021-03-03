using System;
using System.Collections.Generic;

namespace App.Entities.Swagger
{
    public class EndPoint
    {
        public ApiPath Path { get; set; }
        public HTTPVERBS Verb { get; set; }
        public int[] Responses { get; set; }

        #region Constructors
        public EndPoint() { }

        public EndPoint(ApiPath path, HTTPVERBS verb)
        {
            Path = path;
            Verb = verb;
        }
        #endregion

        #region Static Methods
        public static EndPoint[] Create(string path, params HTTPVERBS[] verbs)
        {
            var endPoints = new List<EndPoint>();
            foreach (var verb in verbs)
            {
                endPoints.Add(new EndPoint(new ApiPath(path), verb));
            }
            return endPoints.ToArray();            
        }

        public static EndPoint[] Create(HTTPVERBS verb, params string[] paths)
        {
            var endPoints = new List<EndPoint>();
            foreach (var path in paths)
            {
                endPoints.Add(new EndPoint(new ApiPath(path), verb));
            }
            return endPoints.ToArray();
        }
        #endregion

        #region Override
        public override string ToString()
        {
            return $"[{Verb.ToString()}] {Path.ToString()}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is EndPoint))
                return false;

            var other = (EndPoint)obj;
            return Path.Equals(other.Path) && Verb.Equals(other.Verb);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Path, Verb);
        }
        #endregion
    }
}