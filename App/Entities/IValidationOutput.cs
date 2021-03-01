using System.Collections.Generic;

namespace App.Entities
{
    public interface IValidationOutput
    {
        IEnumerable<string> Messages { get; }
        IEnumerable<string> Problems { get; }
        bool Ok { get; }

        void AddProblem(string message);
    }

    public interface IValidationOutput<T> : IValidationOutput
    {
        T Value { get; }
    }
}