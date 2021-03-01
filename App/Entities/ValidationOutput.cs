using System.Collections.Generic;
using System.Linq;

namespace App.Entities
{
    public class ValidationOutput : IValidationOutput
    {
        private List<Message> messages = new List<Message>();

        public IEnumerable<string> Messages => messages.Select(m => m.Text);
        public IEnumerable<string> Problems => messages.Where(p => p.IsProblem).Select(m => m.Text);
        public bool Ok => !Problems.Any();

        public void AddMessage(string text) => messages.Add(Message.CreateMessage(text));
        public void AddProblem(string text) => messages.Add(Message.CreateProblem(text));

        public void Concat(params ValidationOutput[] outputs)
        {
            foreach (var output in outputs)
            {
                messages.AddRange(output.messages);
            }           
        }

        private struct Message
        {
            public string Text { get; set; }
            public MessageType Type { get; set; }

            private Message(string text, MessageType type)
            {
                Text = text;
                Type = type;
            }

            public bool IsMessage => Type == MessageType.MESSAGE;
            public bool IsProblem => Type == MessageType.PROBLEM;

            public static Message CreateMessage(string text) => new Message(text, MessageType.MESSAGE);
            public static Message CreateProblem(string text) => new Message(text, MessageType.PROBLEM);

            public enum MessageType { MESSAGE, PROBLEM }
        }
    }

    public class ValidationOutput<T> : ValidationOutput, IValidationOutput<T>
    {
        public T Value { get; set; }
    }
}