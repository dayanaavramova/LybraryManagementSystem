using LibraryManagementSystem.Core.Models.Librarian;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Book
{
	public class BookDetailsServiceModel : BookServiceModel
	{
		public string Genre { get; set; } = null!;

        public LibrarianServiceModel Librarian { get; set; }

		[Display(Name = "Published on")]
        public string DatePublished { get; set; }
    }
}
