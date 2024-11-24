using LibraryManagementSystem.Core.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Review
{
	public class AllReviewsModel
	{
		public IEnumerable<ReviewsServiceModel> Reviews { get; set; } = new List<ReviewsServiceModel>();
	}
}
