using LibraryManagementSystem.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
	public class ReviewController : Controller
	{
		private readonly IBookService bookService;
		private readonly IMemberService memberService;
		private readonly IReviewService reviewService;

		public ReviewController(IBookService _bookService, IMemberService _memberService, IReviewService _reviewService)
		{
			bookService = _bookService;
			memberService = _memberService;
			reviewService = _reviewService;
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
	}
}
