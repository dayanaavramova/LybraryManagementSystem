using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Infrastructure.Data.Models
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(NumberMaxLenght)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime MembershipDate { get; set; }

        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; } = null!;

        public IEnumerable<Loan> Loans { get; set; } = new List<Loan>();
        public IEnumerable<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
