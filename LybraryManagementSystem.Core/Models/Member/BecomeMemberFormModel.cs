using System.ComponentModel.DataAnnotations;
using static LibraryManagementSystem.Core.Constants.MessageConstants;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Core.Models.Member
{
	public class BecomeMemberFormModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(NumberMaxLenght,
		MinimumLength = NumberMinLenght,
			ErrorMessage = LengthMessage)]
		[Display(Name = "Phone number")]
		[Phone]
		public string PhoneNumber { get; set; } = null!;
	}
}
