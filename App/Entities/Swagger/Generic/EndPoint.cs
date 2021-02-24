namespace App.Entities.Swagger
{
    public class EndPoint
    {
        public ApiPath Path { get; set; }
        public HTTPVERBS Verb { get; set; }

        public override string ToString()
        {
            return $"[{Verb.ToString()}] {Path.ToString()}";
        }
    }
}