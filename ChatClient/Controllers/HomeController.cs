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
        [Route("/home/"), Route("")]
		public async Task<IActionResult> Index()
		{
            ViewBag.ChannelName = "Home Page";
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateChannel(ChatChannelRequest request)
        {
            await _talkApiService.CreateChannel(request);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Channels/{Name}")]
        public async Task<IActionResult> GetChannel(string Name)
        {
            var messages = await _talkApiService.GetMessages(Name);
            ViewBag.ChannelName = Name;
            return PartialView("ChannelViewPartial", messages);
        }

        [HttpPost]
        [Route("/Home/SendMessage")]
        public async Task<IActionResult> SendMessage(ChatMessageRequest request)
        {
            await _talkApiService.SendMessage(request);
            return RedirectToAction("GetChannel", new { Name = request.channel });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
