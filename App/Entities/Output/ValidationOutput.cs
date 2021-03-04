using System.Collections.Generic;
using System.Linq;

namespace App.Entities.Output
{
    public class ValidationOutput : IValidationOutput
    {
        private List<Message> messages = new List<Message>();

        public IEnumerable<Message> Messages => messages;
        public IEnumerable<Message> Problems => messages.Where(p => p.IsProblem);
        public bool Ok => !Problems.Any();

        public void AddMessage(string text) => messages.Add(new Message(text, false));
        public void AddProblem(string text) => messages.Add(new Message(text, true));

        public void Concat(params IValidationOutput[] outputs)
        {
            foreach (var output in outputs)
            {
                messages.AddRange(output.Messages);
            }           
        }
    }

    public class ValidationOutput<T> : ValidationOutput, IValidationOutput<T>
    {
        public T Value { get; set; }
    }
}