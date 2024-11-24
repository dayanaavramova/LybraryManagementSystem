using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Contracts
{
	public interface IReviewService
	{
		Task<AllReviewsModel> ReviewsByBookIdAsync(int bookId);
		Task<int> CreateAsync(ReviewFormModel model, int memberId);
	}
}
