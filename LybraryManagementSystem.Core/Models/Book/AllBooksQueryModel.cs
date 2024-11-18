using LibraryManagementSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Core.Enumerations;

namespace LibraryManagementSystem.Core.Models.Book
{
	public class AllBooksQueryModel
	{
		public int BooksPerPage { get; } = 5;

		public string Genre { get; set; } = null!;

		public BookSorting Sorting { get; set; }

		public int CurrentPage { get; set; } = 1;

        public int TotalBooksCount { get; set; }

        public IEnumerable<string> Genres { get; set; } = null!;

		public string LibrarianName { get; set; }

		public IEnumerable<BookServiceModel> Books { get; set; } = new List<BookServiceModel>();
    }
}
