using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Librarian;
using LibraryManagementSystem.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    public class LibrarianController : BaseController
    {
        private readonly ILibrarianService librarianService;
        private readonly IRepository repository;

        public LibrarianController(ILibrarianService _librarianService, IRepository _repository)
        {
            librarianService = _librarianService;
            repository = _repository;
        }

        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                return BadRequest();
            }

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
