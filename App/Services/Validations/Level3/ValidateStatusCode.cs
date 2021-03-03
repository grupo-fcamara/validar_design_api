using System;
using System.Collections.Generic;
using System.Linq;
using App.Entities;
using App.Entities.Swagger;
using App.Util;
using Humanizer;

namespace App.Services.Validations.Level3
{
    public class ValidateStatusCode : IValidateStatusCode
    {
        private StatusCodePerVerb _allowedCodesPerVerb = StatusCodePerVerb.Empty;

        public ValidateStatusCode(StatusCodePerVerb allowedCodesPerVerb)
        {
            _allowedCodesPerVerb = allowedCodesPerVerb;
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            var endPoints = documentation.EndPoints;

            foreach (var pathGroup in endPoints.GroupBy(e => e.Path.ToString()))
            {
                var path = pathGroup.Key;
                Dictionary<HTTPVERBS, int[]> notAllowed = StatusCodePerVerb.Empty;

                foreach (var endPoint in pathGroup)
                {
                    var allowedCodes = _allowedCodesPerVerb[endPoint.Verb];

                    var incremented = notAllowed[endPoint.Verb].ToList();
                    incremented.AddRange(endPoint.Responses.Except(allowedCodes));
                    notAllowed[endPoint.Verb] = incremented.Distinct().ToArray();
                }

                notAllowed = notAllowed
                    .Where(pair => pair.Value.Length > 0)
                    .ToDictionary(pair => pair.Key, pair => pair.Value);

                if (notAllowed.Count() > 0)
                {
                    var notAllowedToString = string.Concat(notAllowed.Select(pair => $"{pair.Key} [{pair.Value.Humanize()}] "));
                    output.AddProblem($"Path {path} can return status codes that are not allowed: {notAllowedToString}");
                }                    
            }

            return output;
        }
    }
}