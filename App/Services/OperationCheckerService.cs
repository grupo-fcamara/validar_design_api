using System.Linq;
using Humanizer;

namespace App.Services
{
    public static class OperationCheckerService
    {
        #region Examples
        static string[] english = new string[] 
        {
            "create", "delete", "remove", "accept", "reject", "add", "get", "find"
        };

        static string[] englishContains = new string[] 
        {
            "deleteBy", "getBy", "findBy", "removeBy"
        };

        static string[] portugueseContains = new string[] 
        {
            "adicionar", "remover", "buscar", "encontrar", "encerrar", "iniciar", "finalizar", "aceitar",
            "recusar", "convidar", "confirmar"
        };
        #endregion

        public static bool IsOperation(string text)
        {
            return english.Any(s => Equal(text, s))
                || englishContains.Any(s => Contains(text, s))
                || portugueseContains.Any(s => Contains(text, s));
        }

        private static bool Contains(string text, string content)
            => text.Underscore().Contains(content.Underscore());

        private static bool Equal(string text1, string text2)
            => text1.Underscore().Equals(text2.Underscore());
    }
}