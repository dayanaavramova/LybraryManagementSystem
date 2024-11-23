using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Librarian;
using LibraryManagementSystem.Infrastructure.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static LibraryManagementSystem.Core.Constants.MessageConstants;

namespace LibraryManagementSystem.Controllers
{
    public class LibrarianController : Controller
    {
        private readonly ILibrarianService librarianService;

        public LibrarianController(ILibrarianService _librarianService)
        {
            librarianService = _librarianService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Become()
        {
            if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                return BadRequest();
            }

            if (await librarianService.UserHasRoleAsync(GetUserId()))
            {
                return BadRequest();
            }

            var model = new BecomeLibrarianFormModel();

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Become(BecomeLibrarianFormModel model)
        {
            if (await librarianService.ExistsByIdAsync(GetUserId()))
            {
                return BadRequest();
            }

			if (await librarianService.UserHasRoleAsync(GetUserId()))
			{
				return BadRequest();
			}

			if (await librarianService.UserWithPhoneNumberExistsAsync(model.PhoneNumber))
            {
                ModelState.AddModelError(nameof(model.PhoneNumber), PhoneExists);
            }

            if (ModelState.IsValid == false)
            {
                return View(model);
            }

            await librarianService.CreateAsync(GetUserId(), model.PhoneNumber);

			return RedirectToAction("Index", "Home", new { area = "" });
		}

		public string GetUserId()
		{
			return User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
		}
	}
}
