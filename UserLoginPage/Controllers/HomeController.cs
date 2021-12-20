using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UserLoginPage.Models;

namespace UserLoginPage.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Login()
		{
			return View("Login");
		}

		public IActionResult Register()
		{
			return View("Register");
		}

		public IActionResult UserProfilePage(string login) 
		{
			dynamic d = new System.Dynamic.ExpandoObject();
			d.Login = UserController.loginToToken.ToList().Find(x=>x.Value == login).Key;
			return View("UserProfilePage", d);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}