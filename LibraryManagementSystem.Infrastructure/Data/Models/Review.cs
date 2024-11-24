using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Infrastructure.Data.Models
{
	public class Review
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[Range(RatingMinValue, RatingMaxValue)]
		public int Rating { get; set; }

		[MaxLength(CommentMaxLenght)]
		public string Comment { get; set; }

		[Required]
		public int BookId { get; set; }

		[ForeignKey(nameof(BookId))]
		public Book Book { get; set; }

		[Required]
		public int MemberId { get; set; }

		[ForeignKey(nameof(MemberId))]
		public Member Member { get; set; }
	}
}
