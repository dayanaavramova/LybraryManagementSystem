using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Review;
using LibraryManagementSystem.Infrastructure.Data.Common;
using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Services
{
	public class ReviewService : IReviewService
	{
		private readonly IRepository repository;

		public ReviewService(IRepository _repository)
		{
			repository = _repository;
		}

		public async Task<int> CreateAsync(int bookId, ReviewFormModel model, int memberId)
		{
			Review review = new Review()
			{
				Rating = model.Rating,
				Comment = model.Comment,
				MemberId = memberId,
				BookId = bookId
			};

			await repository.AddAsync(review);
			await repository.SaveChangesAsync();

			return review.BookId;
		}

		public async Task<AllReviewsModel> ReviewsByBookIdAsync(int bookId)
		{
			var reviews = await repository.AllReadOnly<Review>()
				.Where(r => r.BookId == bookId)
				.Select(r => new ReviewsServiceModel()
				{
					Comment = r.Comment,
					Rating = r.Rating,
					Book = new Models.Book.BookServiceModel()
					{
						Title = r.Book.Title,
						ImageUrl = r.Book.ImageUrl
					},
					Member = new Models.Member.MemberServiceModel()
					{
						Name = r.Member.User.UserName,
						Email = r.Member.User.Email,
						PhoneNumber = r.Member.User.PhoneNumber
					}
				}).ToListAsync();

			return new AllReviewsModel()
			{
				Reviews = reviews
			};
		}
	}
}
