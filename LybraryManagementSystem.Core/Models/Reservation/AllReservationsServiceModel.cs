using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Models.Reservation
{
	public class AllReservationsServiceModel
	{
		public IEnumerable<ReservationServiceModel> Reservations { get; set; } = new List<ReservationServiceModel>();
	}
}
