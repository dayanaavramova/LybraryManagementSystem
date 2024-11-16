using LibraryManagementSystem.Core.Contracts;
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
    public class LibrarianService : ILibrarianService
    {
        private readonly IRepository repository;

        public LibrarianService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task CreateAsync(string userId, string phoneNumber)
        {
            await repository.AddAsync(new Librarian() { UserId = userId, PhoneNumber = phoneNumber });
            await repository.SaveChangesAsync();
        }

        public async Task<bool> ExistsByIdAsync(string userId)
        {
            return await repository.AllReadOnly<Librarian>()
                .AnyAsync(a => a.UserId == userId);
        }

        public async Task<bool> UserWithPhoneNumberExistsAsync(string phoneNumber)
        {
            return await repository.AllReadOnly<Librarian>()
                .AnyAsync(a => a.PhoneNumber == phoneNumber);
        }
    }
}
