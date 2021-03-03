namespace App.Entities.Output
{
    public struct Message
    {
        public string Text { get; set; }
        public bool IsProblem { get; set; }

        public Message(string text, bool isProblem)
        {
            Text = text;
            IsProblem = isProblem;
        }
    }
}