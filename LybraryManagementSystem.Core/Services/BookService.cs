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

		public async Task<IEnumerable<AllBooksQueryModel>> AllBooksAsync()
		{
			return await repository
                .AllReadOnly<Book>()
                .Select(b => new AllBooksQueryModel()
                {

                }).ToListAsync();
		}

		public async Task<IEnumerable<BookGenreServiceModel>> AllGenresAsync()
		{
            return await repository.AllReadOnly<Genre>()
                .Select(g => new BookGenreServiceModel()
                {
                    Id = g.Id,
                    Name = g.Name
                }).ToListAsync();
		}

		public async Task<int> CreateAsync(BookFormModel model, int librarianId)
		{
			Book book = new Book()
            {
                Title = model.Title,
                Author = model.Author,
                ISBN = model.ISBN,
                GenreId = model.GenreId,
                LibrarianId = librarianId,
                PublishedDate = model.PublishedDate,
                CopiesAvailable = model.CopiesAvailable,
                ImageUrl = model.ImageUrl
            };

            await repository.AddAsync(book);
            await repository.SaveChangesAsync();

            return book.Id;
        }

		public async Task<bool> GenreExistsAsync(int genreId)
		{
            return await repository.AllReadOnly<Genre>()
                .AnyAsync(c => c.Id == genreId);
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
