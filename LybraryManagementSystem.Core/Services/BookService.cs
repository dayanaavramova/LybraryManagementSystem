using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Home;
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
    public class BookService : IBookService
    {
        private readonly IRepository repository;

        public BookService(IRepository _repository)
        {
            repository = _repository;
        }

		public async Task<IEnumerable<AllBooksModel>> AllBooksAsync()
		{
			return await repository
                .AllReadOnly<Book>()
                .Select(b => new AllBooksModel()
                {

                }).ToListAsync();
		}

		public async Task<IEnumerable<BookIndexServiceModel>> LastThreeBooksAsync()
        {
            return await repository
                .AllReadOnly<Book>()
                .OrderByDescending(b => b.Id)
                .Take(3)
                .Select(b => new BookIndexServiceModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl
                }).ToListAsync();
        }
    }
}
