using System.Collections.Generic;
using System.Linq;

namespace App.Entities
{
    public class ValidationOutput : IValidationOutput
    {
        private List<string> problems = new List<string>();

        public IEnumerable<string> Problems => problems;
        public bool Ok => problems.Count < 1;

        public static IValidationOutput Join(params IValidationOutput[] outputs)
        {
            var output = new ValidationOutput();
            outputs.ToList().ForEach(o => output.Concat(o));
            return output;
        }

        public void AddProblem(string message)
        {
            problems.Add(message);
        }

        public void Concat(params IValidationOutput[] outputs)
        {
            foreach (var output in outputs)
            {
                problems.AddRange(output.Problems);
            }           
        }
    }
}