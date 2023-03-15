using HackTogetherMSGraph.Models;
using HackTogetherMSGraph.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Diagnostics;

namespace HackTogetherMSGraph.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserProfile _userProfile;
        private readonly UserEmails _userEmails;

        public HomeController(ILogger<HomeController> logger, UserProfile userProfile, UserEmails userEmails)
        {
            _logger = logger;
            _userProfile = userProfile;
            _userEmails = userEmails;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        public async Task <IActionResult> Privacy()
        {
            User currentUser = await  _userProfile.GetUserProfile();
            ViewData["GraphApiResult"] = currentUser.DisplayName;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}