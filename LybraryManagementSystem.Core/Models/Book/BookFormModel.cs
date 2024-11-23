using System.ComponentModel.DataAnnotations;
using static LibraryManagementSystem.Core.Constants.MessageConstants;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Core.Models.Book
{
    public class BookFormModel
	{
		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookTitleMaxLenght, MinimumLength = BookTitleMinLenght,
			ErrorMessage = LengthMessage)]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookAuthorMaxLenght, MinimumLength = BookAuthorMinLenght,
			ErrorMessage = LengthMessage)]
		public string Author { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[StringLength(BookISBNMaxLenght, ErrorMessage = LengthMessage)]
		public string ISBN { get; set; } = null!;

		[Required(ErrorMessage = RequiredMessage)]
		[Display(Name = "Is Loaned")]
		public bool IsLoaned { get; set; }

		[Required(ErrorMessage = RequiredMessage)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Display(Name = "Published Date")]
		public DateTime PublishedDate { get; set; }

		[Display(Name = "Genre")]
		public int GenreId { get; set; }

		public IEnumerable<BookGenreServiceModel> Genres { get; set; } =
			new List<BookGenreServiceModel>();
	}
}
