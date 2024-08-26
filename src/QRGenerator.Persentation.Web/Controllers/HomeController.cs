using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;


namespace TrollMarket.Persentation.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "qrgenerator,qrreader")]
        public IActionResult Index()
        {
            bool showNotification = TempData["DataLogin"] != null && (bool)TempData["DataLogin"];

            if (showNotification)
            {
                ViewData["ShowNotification"] = true;
            }
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            return View("index");
        }

        [Route("{*url}", Order = int.MaxValue)]
        public IActionResult HandleUnknownRoutes()
        {
            return RedirectToAction("AccesDanied", "Auth");
        }
    }
}