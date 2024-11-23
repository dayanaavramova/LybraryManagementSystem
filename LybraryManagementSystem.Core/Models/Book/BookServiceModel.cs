using System.ComponentModel.DataAnnotations;
using static LibraryManagementSystem.Core.Constants.MessageConstants;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Core.Models.Book
{
    public class BookServiceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(BookTitleMaxLenght, MinimumLength = BookTitleMinLenght,
            ErrorMessage = LengthMessage)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(BookAuthorMaxLenght, MinimumLength = BookAuthorMinLenght,
            ErrorMessage = LengthMessage)]
        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(BookISBNMaxLenght, ErrorMessage = LengthMessage)]
        public string ISBN { get; set; } = string.Empty;

        [Display(Name = "Is Loaned")]
        public bool IsLoaned { get; set; }
    }
}
