using System.Collections.Generic;
using App.Entities.Output;

namespace App.Entities
{
    public interface IValidationOutput
    {
        IEnumerable<Message> Messages { get; }
        IEnumerable<Message> Problems { get; }
        bool Ok { get; }

        void AddProblem(string message);
        void Concat(params IValidationOutput[] outputs);
    }

    public interface IValidationOutput<T> : IValidationOutput
    {
        T Value { get; }
    }
}