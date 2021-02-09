using System.Linq;

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
            return english.Any(s => text.Equals(s))
                || englishContains.Any(s => text.Contains(s))
                || portugueseContains.Any(s => text.Contains(s));
        }
    }
}