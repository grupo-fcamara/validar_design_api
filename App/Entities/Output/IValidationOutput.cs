using System.Collections.Generic;

namespace App.Entities.Output
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