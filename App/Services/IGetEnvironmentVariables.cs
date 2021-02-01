using App.Entities;
using System.Collections.Generic;

namespace App.Services
{
    public interface IGetEnvironmentVariables
    {
        bool Validate(StructuralData data);
        LANG GetLanguage();
        CASE GetRoutePattern();
        bool GetVersioned();
        HTTPVERBS[] GetHttpVerbs();
        Dictionary<string, int[]> GetStatusCode();
        int GetPathLevels();
        string GetBaseURL();
        string GetSwaggerPath();
    }
}