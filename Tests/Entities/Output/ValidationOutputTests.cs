using System.Linq;
using App.Entities.Output;
using Tests.Util;
using Xunit;

namespace Tests.Entities.Output
{
    public class ValidationOutputTests
    {
        [Theory]
        [InlineData("A", "B")]
        public void TestAddMessage(params string[] messages)
        {
            var output = new ValidationOutput();

            foreach (var message in messages)
                output.AddMessage(message);

            AssertUtil.AllEqual(messages, output.Messages.Select(m => m.Text));
        }

        [Theory]
        [InlineData("A", "B")]
        public void TestAddProblem(params string[] problems)
        {
            var output = new ValidationOutput();

            foreach (var problem in problems)
                output.AddProblem(problem);

            output.AddMessage("Must not appear in problems");
            AssertUtil.AllEqual(problems, output.Problems.Select(m => m.Text));
        }

        [Fact]
        public void TestOk()
        {
            var output = new ValidationOutput();
            output.AddMessage("Must not appear in problems");
            Assert.Empty(output.Problems);
        }

        [Fact]
        public void TestConcat()
        {
            var texts = new string[] { "A", "B", "C", "D" };

            var output = new ValidationOutput();
            output.AddMessage(texts[0]);
            output.AddProblem(texts[1]);

            var output1 = new ValidationOutput();
            output1.AddMessage(texts[2]);
            output1.AddProblem(texts[3]);

            output.Concat(output1);
            AssertUtil.AllEqual(texts, output.Messages.Select(m => m.Text));
        }
    }
}