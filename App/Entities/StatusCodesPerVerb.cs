using System;
using System.Collections.Generic;
using System.Linq;
using App.Util;

namespace App.Entities
{
    public class StatusCodePerVerb : Dictionary<HttpVerbs, int[]>
    {
        public static StatusCodePerVerb Empty
        {
            get
            {
                var codesPerVerb = new StatusCodePerVerb();

                foreach (var value in EnumUtil.GetValues<HttpVerbs>())
                    codesPerVerb[value] = new int[] { };

                return codesPerVerb;
            }
        }

        public static StatusCodePerVerb Parse(Dictionary<HttpVerbs, int[]> dictionary)
        {
            var obj = Empty;
            var verbs = dictionary.Keys;

            foreach (var verb in verbs)
                obj[verb] = dictionary[verb].Distinct().ToArray();

            return obj;
        }

        public static StatusCodePerVerb Parse(Dictionary<string, int[]> dictionary)
        {
            return Parse(dictionary
                .ToDictionary(
                    pair => Enum.Parse<HttpVerbs>(pair.Key), 
                    pair => pair.Value
                )
            );
        }

        public static implicit operator StatusCodePerVerb(Dictionary<string, int[]> dictionary) => Parse(dictionary);
    }
}