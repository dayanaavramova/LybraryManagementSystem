using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static LibraryManagementSystem.Core.Constants.MessageConstants;
using static LibraryManagementSystem.Infrastructure.Constants.DataConstants;

namespace LibraryManagementSystem.Core.Models.Librarian
{
    public class BecomeLibrarianFormModel
    {
        [Required(ErrorMessage = RequiredMessage)]
        [StringLength(NumberMaxLenght,
            MinimumLength = NumberMinLenght,
            ErrorMessage = LengthMessage)]
        [Display(Name = "Phone number")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;
    }
}
