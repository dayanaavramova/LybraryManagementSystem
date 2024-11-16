using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Contracts
{
	public interface IMemberService
	{
		Task<bool> ExistsByIdAsync(string userId);
		Task CreateAsync(string userId, string phoneNumber);
		Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber);
		Task<bool> UserHasRoleAsync(string userId);
	}
}
