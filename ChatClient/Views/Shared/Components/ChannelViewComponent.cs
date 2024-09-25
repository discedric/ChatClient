using ChatClient.Service;
using Microsoft.AspNetCore.Mvc;

namespace ChatClient.Views.Shared.Components
{
    public class ChannelViewComponent(TalkApiService talkApiService) : ViewComponent
    {
        private readonly TalkApiService _talkApiService = talkApiService;

        public async Task<IViewComponentResult> InvokeAsync(string type)
        {
            var channels = await _talkApiService.GetChannels();
            return View(type, channels);
        }
    }
}
