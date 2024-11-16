using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Member;
using LibraryManagementSystem.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static LibraryManagementSystem.Core.Constants.MessageConstants;

namespace LibraryManagementSystem.Controllers
{
	public class MemberController : Controller
	{
		private readonly IMemberService memberService;

		public MemberController(IMemberService _memberService)
		{
			memberService = _memberService;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			if (await memberService.ExistsByIdAsync(GetUserId()))
			{
				return BadRequest();
			}

			if (await memberService.UserHasRoleAsync(GetUserId()))
			{
				return BadRequest();
			}

			var model = new BecomeMemberFormModel();

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeMemberFormModel model)
		{
			if (await memberService.ExistsByIdAsync(GetUserId()))
			{
				return BadRequest();
			}

			if (await memberService.UserHasRoleAsync(GetUserId()))
			{
				return BadRequest();
			}

			if (await memberService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
			}

			if (ModelState.IsValid == false)
			{
				return View(model);
			}

			await memberService.CreateAsync(GetUserId(), model.PhoneNumber);

			return RedirectToAction("Index", "Home", new { area = "" });
		}

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
