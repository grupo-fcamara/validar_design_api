namespace App.Entities
{
    public class EndPoint
    {
        public ApiPath Path { get; set; }
        public HttpVerbs Verb { get; set; }
        public int[] Responses { get; set; }
    }
}