using ChatClient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ChatClient.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChatClient.Controllers
{
	public class HomeController(TalkApiService talkApiService) : Controller
	{
        private readonly TalkApiService _talkApiService = talkApiService;
        public string Chatter;
        [HttpGet]
        [Route("/home/"), Route("")]
		public async Task<IActionResult> Index()
        {
            ViewBag.ChannelName = "Home page";
            return View();
		}

		[HttpPost]
		public async Task<IActionResult> CreateChannel(string Name)
        {
            ChatChannelRequest chatChannelRequest = new ChatChannelRequest() { Name = Name};
            await _talkApiService.CreateChannel(chatChannelRequest);
            return RedirectToAction("Index");
        }

        [HttpGet]
        //[Route("/Channels/{Name}")]
        public async Task<IActionResult> GetChannel(string Name)
        {
            var messages = await _talkApiService.GetMessages(Name);
            ViewBag.ChannelName = Name;
            return View("Index", messages);
        }

        [HttpPost]
        [Route("/Home/SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageRequest request)
        {
            if (string.IsNullOrEmpty(request.message))
            {
                return BadRequest("Message cannot be empty.");
            }

            // Retrieve the Chatter value from the cookie
            if (Request.Cookies.TryGetValue("Chatter", out var chatter))
            {
                request.author = chatter;
            }
            else
            {
                request.author = "Anonymous";
            }

            await _talkApiService.SendMessage(request);
            return RedirectToAction("GetChannel", new { Name = request.channel });
        }

        public void SetUser(string user)
        {
            Chatter = user;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
