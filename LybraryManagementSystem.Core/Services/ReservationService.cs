using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Reservation;
using LibraryManagementSystem.Core.Models.Review;
using LibraryManagementSystem.Infrastructure.Data.Common;
using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Services
{
	public class ReservationService : IReservationService
	{
		private readonly IRepository repository;

		public ReservationService(IRepository _repository)
		{
			repository = _repository;
		}

		public async Task CreateAsync(int bookId, int memberId)
		{
			Reservation reservation = new Reservation()
			{
				MemberId = memberId,
				BookId = bookId
			};

			await repository.AddAsync(reservation);
			await repository.SaveChangesAsync();
		}

		public async Task DeleteAsync(int id, int memberId)
		{
			var reservationId = await repository.AllReadOnly<Reservation>()
				.Where(r => r.BookId == id && r.MemberId == memberId)
				.Select(r => r.Id).FirstOrDefaultAsync();

			if (reservationId != null)
			{
				await repository.DeleteAsync<Reservation>(reservationId);
				await repository.SaveChangesAsync();
			}
		}

		public async Task<bool> HasBookBeenReservedByMember(int bookId, int memberId)
		{
			return await repository.AllReadOnly<Reservation>()
				.AnyAsync(r => r.BookId == bookId && r.MemberId == memberId);
		}

		public async Task<AllReservationsServiceModel> ReservationsBymemberIdAsync(int memberId)
		{
			var reservations = await repository.AllReadOnly<Reservation>()
				.Where(r => r.MemberId == memberId)
				.Select(r => new ReservationServiceModel()
				{
					Book = new Models.Book.BookServiceModel()
					{
						Title = r.Book.Title,
						ImageUrl = r.Book.ImageUrl,
						Id = r.Book.Id
					}
				}).ToListAsync();

			return new AllReservationsServiceModel()
			{
				Reservations = reservations
			};
		}
	}
}
