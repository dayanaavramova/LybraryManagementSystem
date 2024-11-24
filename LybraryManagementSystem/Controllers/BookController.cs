using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Book;
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
                ModelState.AddModelError(nameof(model.GenreId), "Category does not exist.");
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
            if (await bookService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }
            if (await bookService.HasLibrarianWithIdAsync(id, GetUserId()) == false)
            {
                return Unauthorized();
            }

            var model = await bookService.GetBookFormModelByIdAsync(id);
            
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookFormModel model)
        {
			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (await bookService.HasLibrarianWithIdAsync(id, GetUserId()) == false)
			{
				return Unauthorized();
			}
			if (await bookService.GenreExistsAsync(model.GenreId) == false)
			{
				ModelState.AddModelError(nameof(model.GenreId), "Category does not exist.");
			}
            if (!ModelState.IsValid)
            {
                model.Genres = await bookService.AllGenresAsync();

                return View(model);
            }

            await bookService.EditAsync(id, model);

			return RedirectToAction(nameof(Details), new { id = id });
		}

        [Authorize]
        public async Task<IActionResult> Mine()
        {
            if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                var currentLibrarianId = await librarianService.GetLibrarianIdAsync(GetUserId());

                var myBooks = await bookService.AllBooksByLibrarianIdAsync(currentLibrarianId);

                return View(myBooks);
            }
            else if (await memberService.ExistsByIdAsync(GetUserId()))
            {
				var currentMemberId = await memberService.GetMemberIdAsync(GetUserId());

				var myBooks = await bookService.AllBooksByMemberIdAsync(currentMemberId);

                return View(myBooks);
			}
            else
            {
                return BadRequest();
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Loan(int id)
        {
			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                return Unauthorized();
            }
            if (await bookService.IsBookLoanedAsync(id))
            {
                return BadRequest();
			}

            var currentMemberId = memberService.GetMemberIdAsync(GetUserId()).Result;
            if (currentMemberId == null)
            {
               return Unauthorized();
            }

            await bookService.LoanAsync(id, currentMemberId ?? 0);

            return RedirectToAction(nameof(Mine));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Return(int id)
        {
            var memberId = memberService.GetMemberIdAsync(GetUserId()).Result;
			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}
            if (await bookService.IsBookLoanedByMemberWithIdAsync(id, memberId) == false)
            {
                return Unauthorized();
            }

            await bookService.ReturnAsync(id);

			return RedirectToAction(nameof(All));
		}

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await bookService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }
            if (await bookService.HasLibrarianWithIdAsync(id, GetUserId()) == false)
            {
                return Unauthorized();
            }

            var book = bookService.BookDetailsByIdAsync(id);
            var model = new BookDetailsViewModel()
            {
                Id = book.Id,
                Title = book.Result.Title,
                Author = book.Result.Author,
                ISBN = book.Result.ISBN,
                ImageUrl = book.Result.ImageUrl,
                PublishedDate = book.Result.DatePublished
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Delete(BookDetailsViewModel model)
        {
			if (await bookService.ExistsAsync(model.Id) == false)
			{
				return BadRequest();
			}
			if (await bookService.HasLibrarianWithIdAsync(model.Id, GetUserId()) == false)
			{
				return Unauthorized();
			}

            await bookService.DeleteAsync(model.Id);

			return RedirectToAction(nameof(All));
        }

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
