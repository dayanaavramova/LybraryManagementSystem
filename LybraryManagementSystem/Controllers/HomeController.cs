using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Services;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }


        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var model = await _bookService.LastThreeBooksAsync();

            return View(model);
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 404)
            {
                return View("Error404");
            }
			if (statusCode == 500)
			{
				return View("Error500");
			}

			return View();
        }
    }
}