using LibraryManagementSystem.Core.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Humanizer.In;

namespace LibraryManagementSystem.Controllers
{
	public class ReservationController : Controller
	{
		private readonly IBookService bookService;
		private readonly IMemberService memberService;
		private readonly IReservationService reservationService;
		private readonly ILibrarianService librarianService;

		public ReservationController(IBookService _bookService, IMemberService _memberService, IReservationService _reservationService, ILibrarianService _librarianService)
		{
			bookService = _bookService;
			memberService = _memberService;
			librarianService = _librarianService;
			reservationService = _reservationService;
		}


		[Authorize]
		[HttpGet]
		public async Task<IActionResult> All()
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
			var model = await reservationService.ReservationsBymemberIdAsync(memberId ?? 0);

			return View(model);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Add(int id)
		{
			int? memberId = await memberService.GetMemberIdAsync(GetUserId());

			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (await librarianService.ExistsByIdAsync(GetUserId()))
			{
				return Unauthorized();
			}
			if (await reservationService.HasBookBeenReservedByMember(id, memberId ?? 0))
			{
				return BadRequest();
			}

			if (memberId == null)
			{
				return Unauthorized();
			}

			await reservationService.CreateAsync(id, memberId ?? 0);

			return RedirectToAction(nameof(All));
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> Remove(int id)
		{
			int? memberId = memberService.GetMemberIdAsync(GetUserId()).Result;
			if (await bookService.ExistsAsync(id) == false)
			{
				return BadRequest();
			}
			if (await reservationService.HasBookBeenReservedByMember(id, memberId ?? 0) == false)
			{
				return Unauthorized();
			}

			await reservationService.DeleteAsync(id, memberId ?? 0);

			return RedirectToAction(nameof(All));
		}

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
