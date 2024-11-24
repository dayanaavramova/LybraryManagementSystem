using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Member;
using System.ComponentModel.DataAnnotations;
using static LibraryManagementSystem.Core.Constants.MessageConstants;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Core.Models.Review
{
	public class ReviewsServiceModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[Range(RatingMinValue, RatingMaxValue)]
		public int Rating { get; set; }

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(CommentMaxLenght, MinimumLength = CommentMinLenght,
			ErrorMessage = LengthMessage)]
		public string Comment { get; set; }

		public BookServiceModel Book { get; set; }

		public MemberServiceModel Member { get; set; }
	}
}
