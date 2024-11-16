using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Librarian;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class LibrarianController : BaseController
    {
        private readonly ILibrarianService librarianService;

        public LibrarianController(ILibrarianService _librarianService)
        {
            librarianService = _librarianService;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            var model = new BecomeLibrarianFormModel();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Become(BecomeLibrarianFormModel model)
        {
            //return await RedirectToAction(nameof(BookController.All), "Book");
            return View();
        }
    }
}
