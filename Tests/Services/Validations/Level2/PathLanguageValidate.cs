using Xunit;
using System.Linq;
using App.Services.Validations.Level2;
using App.Entities.Environment;
using Tests.Services.Validations;
using System.IO;
using System;

namespace Tests
{ 
    public class PathLanguageValidate : Validation
    {
        [Theory]
        [InlineData(1, Language.PORTUGUESE, "users/{id}", "usuarios/{id}")]
        public void ReturnProperly(int expectedProblems, Language language, params string[] paths) 
        {
            Prepare();
            var output = ReturnProblems(new ValidatePathLanguage(language), paths);
            Assert.Equal(expectedProblems, output.Problems.Count()); 
        }

        private void Prepare()
        {
            string xmlPath = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "Core14.profile.xml").Replace("Tests", "App");
            string path = Path.Combine(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName, "Core14.profile.xml");
            
            if (!File.Exists(path))
                File.Copy(xmlPath, path);
        }
    }
} 