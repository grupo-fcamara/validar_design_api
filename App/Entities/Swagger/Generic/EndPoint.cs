namespace App.Entities.Swagger
{
    public class EndPoint
    {
        public ApiPath Path { get; set; }
        public HTTPVERBS Verb { get; set; }
    }
}