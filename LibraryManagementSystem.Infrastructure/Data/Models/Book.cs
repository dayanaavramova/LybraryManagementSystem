using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Infrastructure.Data.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(BookTitleMaxLenght)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(BookAuthorMaxLenght)]
        public string Author { get; set; } = string.Empty;

        [Required]
        public int GenreId { get; set; }

        [ForeignKey(nameof(GenreId))]
        public Genre Genre { get; set; } = null!;

        [Required]
        public int LibrarianId { get; set; }

        [ForeignKey(nameof(LibrarianId))]
        public Librarian Librarian { get; set; } = null!;

        [Required]
        [MaxLength(BookISBNMaxLenght)]
        public string ISBN { get; set; } = string.Empty;

        [Required]
        public DateTime PublishedDate { get; set; }

        [Required]
        public int CopiesAvailable { get; set; }

        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
