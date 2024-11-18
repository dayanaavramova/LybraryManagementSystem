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

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> All([FromQuery]AllBooksQueryModel query)
        {
            var model = await bookService.AllAsync(query.Genre, query.Sorting, query.CurrentPage, query.BooksPerPage);

            query.TotalBooksCount = model.TotalBooksCount;
            query.Books = model.Books;
            query.Genres = await bookService.AllGenresNamesAsync();
            if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                query.LibrarianName = User.Identity.Name;
            }

            return View(query);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (await bookService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var model = await bookService.BookDetailsByIdAsync(id);

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormModel)
        {
            return View();
        }

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
