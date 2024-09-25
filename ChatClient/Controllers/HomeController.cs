using ChatClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChatClient.Service;

namespace ChatClient.Controllers
{
	public class HomeController(TalkApiService talkApiService) : Controller
	{
        private readonly TalkApiService _talkApiService = talkApiService;
        [HttpGet]
		public async Task<IActionResult> Index()
		{
			var channels = await _talkApiService.GetChannels();
			return View(channels);
		}

		[HttpPost]
		public async Task<IActionResult> CreateChannel(ChatChannelRequest request)
        {
            await _talkApiService.CreateChannel(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetChannel(string Name)
        {
            var messages = await _talkApiService.GetMessages(Name);
            return PartialView("ChannelViewPartial", messages);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
