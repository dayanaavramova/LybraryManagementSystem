using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Reservation;
using LibraryManagementSystem.Core.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Contracts
{
	public interface IReservationService
	{
		Task<AllReservationsServiceModel> ReservationsBymemberIdAsync(int memberId);
		Task CreateAsync(int bookId, int memberId);
		Task DeleteAsync(int id, int memberId);
		Task<bool> HasBookBeenReservedByMember(int bookId, int memberId);
	}
}
