using LibraryManagementSystem.Core.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Reservation
{
	public class ReservationServiceModel
	{
		public BookServiceModel Book { get; set; }
	}
}
