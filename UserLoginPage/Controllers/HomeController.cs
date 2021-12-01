﻿using Microsoft.AspNetCore.Mvc;
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}