using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;
        private readonly ILibrarianService librarianService;
        private readonly IMemberService memberService;

        public BookController(IBookService _bookService, ILibrarianService _librarianService, IMemberService _memberService)
        {
            bookService = _bookService;
            librarianService = _librarianService;
            memberService = _memberService;
        }

        [HttpGet]
        public IActionResult All()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = new AllBooksQueryModel();

            return View(model);
        }

        [Authorize]
        [HttpGet]
		public async Task<IActionResult> Add()
        {
			if (await librarianService.ExistsByIdAsync(GetUserId()) == false)
            {
                if (await memberService.ExistsByIdAsync(GetUserId()))
                {
                    return BadRequest();
                }

                return RedirectToAction(nameof(LibrarianController.Become), "Librarian");
            }

            var model = new BookFormModel()
            {
                Genres = await bookService.AllGenresAsync()
            };
            return View(model);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(BookFormModel model)
        {
			if (await librarianService.ExistsByIdAsync(GetUserId()) == false)
			{
				if (await memberService.ExistsByIdAsync(GetUserId()))
				{
					return BadRequest();
				}

				return RedirectToAction(nameof(LibrarianController.Become), "Librarian");
			}

            if (await bookService.GenreExistsAsync(model.GenreId) == false)
            {
                ModelState.AddModelError(nameof(model.GenreId), "");
            }

            if (ModelState.IsValid == false)
            {
                model.Genres = await bookService.AllGenresAsync();

                return View(model);
            }

            int? librarianId = await librarianService.GetLibrarianIdAsync(GetUserId());

            int newBookId = await bookService.CreateAsync(model, librarianId ?? 0);

			return RedirectToAction(nameof(Details), new { id = newBookId });
        }

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
