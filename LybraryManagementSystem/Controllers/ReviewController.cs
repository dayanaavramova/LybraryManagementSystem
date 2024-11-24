using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Review;
using LibraryManagementSystem.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibraryManagementSystem.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IBookService bookService;
		private readonly IMemberService memberService;
		private readonly IReviewService reviewService;
		private readonly ILibrarianService librarianService;

		public ReviewController(IBookService _bookService, IMemberService _memberService, IReviewService _reviewService, ILibrarianService _librarianService)
		{
			bookService = _bookService;
			memberService = _memberService;
			reviewService = _reviewService;
			librarianService = _librarianService;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> All(int id)
		{
			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}

			var model = await reviewService.ReviewsByBookIdAsync(id);

			return View(model);
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> Add(int id)
		{
			if (await memberService.ExistsByIdAsync(GetUserId()) == false)
			{
				if (await librarianService.ExistsByIdAsync(GetUserId()))
				{
					return BadRequest();
				}

				return RedirectToAction(nameof(MemberController.Become), "Member");
			}

			var model = new ReviewFormModel();

			return View(model);
		}


		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Add(int id, ReviewFormModel model)
		{
			if (await memberService.ExistsByIdAsync(GetUserId()) == false)
			{
				if (await librarianService.ExistsByIdAsync(GetUserId()))
				{
					return BadRequest();
				}

				return RedirectToAction(nameof(MemberController.Become), "Member");
			}

			int? memberId = await memberService.GetMemberIdAsync(GetUserId());

			await reviewService.CreateAsync(id, model, memberId ?? 0);

			return RedirectToAction(nameof(All), new { id = id });
		}

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
