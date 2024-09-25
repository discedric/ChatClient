namespace ChatClient.Models
{
    public class ChatMessageRequest
    {
        public required string author { get; set; }
        public required string message { get; set; }
        public required string channel { get; set; }
}
}
