namespace ChatClient.Models
{
    public class ChatMessageResult
    {
        public int id { get; set; }
        public string author { get; set; }
        public string message { get; set; }
        public DateTime createdAt { get; set; }

    }
}
