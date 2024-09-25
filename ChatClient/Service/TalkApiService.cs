using ChatClient.Models;
namespace ChatClient.Service
{
    public class TalkApiService(IHttpClientFactory httpClientFactory)
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory; 
        
        public async Task<IList<ChatChannelResult>> GetChannels()
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "api/chat-channels";
            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var channels = await response.Content.ReadFromJsonAsync<IList<ChatChannelResult>>();
            return channels;
        }

        public async Task CreateChannel(ChatChannelRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "api/chat-channels";
            var response = await httpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<IList<ChatMessageResult>> GetMessages(string ChannelName)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = $"api/chat-messages?Channel={ChannelName}";
            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();
            var messages = await response.Content.ReadFromJsonAsync<IList<ChatMessageResult>>();
            return messages;
        }

        public async Task SendMessage(ChatMessageRequest request)
        {
            var httpClient = _httpClientFactory.CreateClient("TalkApi");
            var route = "api/chat-messages";
            var response = await httpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();
        }
    }
}
