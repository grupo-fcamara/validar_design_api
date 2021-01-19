using System.Collections.Generic;

namespace App.Entities
{
    public interface IValidationOutput
    {
        IEnumerable<string> Problems { get; }
        bool Ok { get; }

        void AddProblem(string message);
        void Concat(params IValidationOutput[] outputs);
    }
}