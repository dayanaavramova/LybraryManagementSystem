using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LibraryManagementSystem.Core.Models.Member
{
	public class MemberServiceModel
	{
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
	}
}
