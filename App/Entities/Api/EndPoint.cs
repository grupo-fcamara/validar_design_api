using System;
using System.Collections.Generic;
using App.Entities.Environment;

namespace App.Entities.Api
{
    public class EndPoint
    {
        public ApiPath Path { get; set; }
        public HttpVerbs Verb { get; set; }
        public EndPointParameter[] Parameters { get; set; } = new EndPointParameter[] { };
        public int[] Responses { get; set; } = new int[] { };

        #region Constructors
        public EndPoint() { }

        public EndPoint(ApiPath path, HttpVerbs verb)
        {
            Path = path;
            Verb = verb;
        }
        #endregion

        #region Static Methods
        public static EndPoint[] Create(string path, params HttpVerbs[] verbs)
        {
            var endPoints = new List<EndPoint>();
            foreach (var verb in verbs)
            {
                endPoints.Add(new EndPoint(new ApiPath(path), verb));
            }
            return endPoints.ToArray();            
        }

        public static EndPoint[] Create(HttpVerbs verb, params string[] paths)
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