using System.Linq;
using App.Entities.Environment;
using App.Entities.Output;
using App.Entities.Swagger;
using NTextCat;

namespace App.Services.Validations.Level2
{
    public class ValidatePathLanguage : IValidatePathLanguage
    {
        private readonly RankedLanguageIdentifier identifier;

        public Language Language { get; private set; }

        public ValidatePathLanguage(Language language)
        {
            Language = language;

            //Loading Language Identifier
            var factory = new RankedLanguageIdentifierFactory();
            identifier = factory.Load("Core14.profile.xml");
        }

        public ValidationOutput Validate(IDocumentation documentation)
        {
            var output = new ValidationOutput();
            string acronym = "";

            if (Language == Language.PORTUGUESE) { acronym = "por"; }
            if (Language == Language.ENGLISH) { acronym = "eng"; }

            foreach (var path in documentation.Paths)
            {
                var parts = path.Parts
                    .Where(part => !part.IsIdentifier);

                var pathIsRespectingLanguage = parts
                    .All(part => WordIsInLanguage(part.ToString(), acronym));

                if (!pathIsRespectingLanguage)
                    output.AddProblem($"Path {path.ToString()} is not fully in {Language.ToString()}");
            }

            return output;
        }

        private bool WordIsInLanguage(string word, string languageAcronym)
        {
            var possibleLanguages = identifier.Identify(word);
            var topLanguages = possibleLanguages.Take(5);

            return topLanguages.Any(lang => lang.Item1.Iso639_3.ToString() == languageAcronym);
        }
    }
}