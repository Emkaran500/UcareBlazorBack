using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UcareApp.Auth.Models;

namespace UcareApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            ViewData["UserName"] = user?.Name ?? "guest"; 
            return View();
        }
    }
}
