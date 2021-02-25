using System;
using System.Linq;
using System.Text.Json;
using System.Collections.Generic;
using App.Entities;
using Humanizer;

namespace App.Services
{
    public class GetEnvironmentVariables : IGetEnvironmentVariables
    {
        private Dictionary<string, string> _errorMessages = new Dictionary<string, string>();

        public GetEnvironmentVariables()
        {
            LoadErrorMessages();         
        }

        public StructuralData GetData() {

            StructuralData data = new StructuralData();
            var exceptions = new List<Exception>();
            var actions = new List<Action>();

            actions.AddRange(
                () => data.Language = GetLanguage(),
                () => data.RoutePattern = GetRoutePattern(),
                () => data.Versioned = IsVersioned(),
                () => data.HttpVerbs = GetHttpVerbs(),
                () => data.StatusCode = GetStatusCodePerVerb(),
                () => data.PathLevels = GetPathLevels(),
                () => data.BaseUrl = GetBaseUrl(),
                () => data.SwaggerPath = GetSwaggerPath()
            );

            foreach (var action in actions)
            {
                try { action(); }
                catch (Exception e) { exceptions.Add(e); }
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions);

            return data;
        }

        #region Getters
        public Language GetLanguage() => 
            GetVariable<Language>("LANGUAGE", value => Enum.Parse<Language>(value));

        public CasePattern GetRoutePattern() => 
            GetVariable<CasePattern>("ROUTE_PATTERN", value => Enum.Parse<CasePattern>(value));

        public bool IsVersioned() => 
            GetVariable<bool>("VERSIONED_PATH", bool.Parse);

        public HttpVerbs[] GetHttpVerbs() => 
            GetVariable<HttpVerbs[]>("HTTP_VERBS", ParseHttpVerbs);

        public StatusCodePerVerb GetStatusCodePerVerb() => 
            GetVariable<StatusCodePerVerb>("STATUS_CODE", ParseStatusCodePerVerb);

        public int GetPathLevels() => 
            GetVariable<int>("PATH_LEVELS", int.Parse);

        public string GetBaseUrl() =>
            Environment.GetEnvironmentVariable("BASE_URL");

        public string GetSwaggerPath() =>
            Environment.GetEnvironmentVariable("SWAGGER_PATH");
        #endregion

        #region Parsers
        private HttpVerbs[] ParseHttpVerbs(string value)
        {
            string[] verbs = JsonSerializer.Deserialize<string[]>(value);
            List<HttpVerbs> list = new List<HttpVerbs>();

            foreach (var str in verbs)
            {
                HttpVerbs result;

                if (!Enum.TryParse(str, true, out result))
                    throw new Exception(_errorMessages["LANGUAGE"]);

                list.Add(result);
            }

            return list.ToArray();
        }

        private StatusCodePerVerb ParseStatusCodePerVerb(string value)
        {
            return JsonSerializer.Deserialize<Dictionary<string, int[]>>(value);
        }
        #endregion

        private T GetVariable<T>(string variable, Func<string, T> parser)
        {
            string value = Environment.GetEnvironmentVariable(variable);

            if (string.IsNullOrWhiteSpace(value))
                throw new Exception(_errorMessages[variable]);

            T result;

            try {
                result = parser(value);
            }
            catch {
                throw new Exception(_errorMessages[variable]);
            }

            return result;
        }

        private void LoadErrorMessages()
        {
            _errorMessages["LANGUAGE"] = "LANGUAGE variable not set properly, available languages:\n"
                + $"{Enum.GetNames(typeof(Language)).Humanize("and")}";

            _errorMessages["ROUTE_PATTERN"] = "ROUTE_PATTERN variable not set properly, available route patterns:\n"
                + $"{Enum.GetNames(typeof(CasePattern)).Humanize("or")}";
            
            _errorMessages["VERSIONED_PATH"] = "VERSIONED_PATH variable not set properly, available values:\n"
                + "\"true\" OR \"false\"";
            
            _errorMessages["HTTP_VERBS"] = "HTTP_VERBS variable not set properly, available HTTP verbs:\n"
                + $"{Enum.GetNames(typeof(HttpVerbs)).Humanize("and")}";
            
            _errorMessages["STATUS_CODE"] = "STATUS_CODE variable not set properly, example on how to set:\n"
                + $"\"{{ \"{HttpVerbs.GET}\": [200, 500], \"{HttpVerbs.POST}\": [200, 500], "
                + $"\"{HttpVerbs.PUT}\": [200, 500], \"{HttpVerbs.DELETE}\": [200, 500] }}\"";
            
            _errorMessages["PATH_LEVELS"] = "PATH_LEVELS variable not set properly, minimum: \"0\"";
            
            _errorMessages["BASE_URL"] = "BASE_URL variable not set properly, example on how to set:\n"
                + "BASE_URL: \"yourbaseurl.com\"";
            
            _errorMessages["SWAGGER_PATH"] = "SWAGGER_PATH variable not set properly, example on how to set:\n"
                + "SWAGGER_PATH: \"yourswaggerpath.com\"";            
        }
    }
}