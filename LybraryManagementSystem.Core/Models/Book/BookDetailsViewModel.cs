using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Book
{
	public class BookDetailsViewModel
	{
        public int Id { get; set; }
		public string Title { get; set; } = string.Empty;
		public string Author { get; set; } = string.Empty;
		public string ISBN { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
		public string PublishedDate { get; set; }
	}
}
