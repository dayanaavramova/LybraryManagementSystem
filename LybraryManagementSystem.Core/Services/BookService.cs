using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Enumerations;
using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Home;
using LibraryManagementSystem.Infrastructure.Constants;
using LibraryManagementSystem.Infrastructure.Data.Common;
using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LibraryManagementSystem.Core.Services
{
	public class BookService : IBookService
    {
        private readonly IRepository repository;

        public BookService(IRepository _repository)
        {
            repository = _repository;
        }

        public async Task<BookQueryServiceModel> AllAsync(string? genre = null, BookSorting sorting = BookSorting.Newest, int currentpage = 1, int booksPerPage = 1)
        {
            var booksToShow = repository.AllReadOnly<Book>();
            
            if (genre != null)
            {
                booksToShow = booksToShow.Where(b => b.Genre.Name == genre);
            }

            booksToShow = sorting switch
            {
                BookSorting.LoanbleFirst => booksToShow
                .OrderBy(b => b.IsLoaned == true),
                BookSorting.Newest => booksToShow
                .OrderByDescending(b => b.Id)
            };

            var books = await booksToShow
                .Skip((currentpage - 1) * booksPerPage)
                .Take(booksPerPage)
                .Select(b => new BookServiceModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    ImageUrl = b.ImageUrl,
                    IsLoaned = b.IsLoaned
                }).ToListAsync();

            int totalBooks = await booksToShow.CountAsync();

            return new BookQueryServiceModel()
            {
                Books = books,
                TotalBooksCount = totalBooks
            };
        }

        public async Task<IEnumerable<AllBooksQueryModel>> AllBooksAsync()
		{
			return await repository
                .AllReadOnly<Book>()
                .Select(b => new AllBooksQueryModel()
                {

                }).ToListAsync();
		}

		public async Task<IEnumerable<BookServiceModel>> AllBooksByLibrarianIdAsync(int? librarianId)
		{
			var books = await repository.AllReadOnly<Book>()
                .Where(b => b.LibrarianId == librarianId)
                .Select(b => new BookServiceModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    ImageUrl = b.ImageUrl,
                    IsLoaned = b.IsLoaned
                })
                .ToListAsync();

            return books;
		}

		public async Task<IEnumerable<BookServiceModel>> AllBooksByMemberIdAsync(int? memberId)
		{
			var books = await repository.AllReadOnly<Book>()
				.Where(b => b.LoanerId == memberId)
				.Select(b => new BookServiceModel()
				{
					Id = b.Id,
					Title = b.Title,
					Author = b.Author,
					ISBN = b.ISBN,
					ImageUrl = b.ImageUrl,
					IsLoaned = b.IsLoaned
				})
				.ToListAsync();

			return books;
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

        public async Task<IEnumerable<string>> AllGenresNamesAsync()
        {
            return await repository.AllReadOnly<Genre>()
                .Select(g => g.Name)
                .Distinct().ToListAsync();
        }

		public async Task<BookDetailsServiceModel> BookDetailsByIdAsync(int id)
		{
            return await repository.AllReadOnly<Book>()
                .Where(b => b.Id == id)
                .Select(b => new BookDetailsServiceModel()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                    ImageUrl = b.ImageUrl,
                    ISBN = b.ISBN,
                    IsLoaned = b.IsLoaned,
                    Genre = b.Genre.Name,
                    DatePublished = b.PublishedDate.ToString(DataConstants.DateFormat),
                    Librarian = new Models.Librarian.LibrarianServiceModel()
                    {
                        PhoneNumber = b.Librarian.PhoneNumber,
                        Email = b.Librarian.User.Email,
                        Name = b.Librarian.User.UserName
                    }
                })
                .FirstOrDefaultAsync();
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
                IsLoaned = false,
                ImageUrl = model.ImageUrl
            };

            await repository.AddAsync(book);
            await repository.SaveChangesAsync();

            return book.Id;
        }

		public async Task DeleteAsync(int id)
		{
			await repository.DeleteAsync<Book>(id);
            await repository.SaveChangesAsync();
		}

		public async Task EditAsync(int bookId, BookFormModel model)
		{
            var book = await repository.GetByIdAsync<Book>(bookId);

            if (book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.ISBN = model.ISBN;
                book.PublishedDate = model.PublishedDate;
                book.IsLoaned = model.IsLoaned;
                book.ImageUrl = model.ImageUrl;
                book.GenreId = model.GenreId;

                await repository.SaveChangesAsync();
            }
		}

		public async Task<bool> ExistsAsync(int id)
		{
			return await repository.AllReadOnly<Book>()
				.AnyAsync(b => b.Id == id);
		}

		public async Task<bool> GenreExistsAsync(int genreId)
		{
            return await repository.AllReadOnly<Genre>()
                .AnyAsync(c => c.Id == genreId);
		}

		public async Task<BookFormModel?> GetBookFormModelByIdAsync(int id)
		{
            var book = await repository.AllReadOnly<Book>()
                .Where(b => b.Id == id)
                .Select(b => new BookFormModel()
                {
                    Title = b.Title,
                    Author = b.Author,
                    ISBN = b.ISBN,
                    PublishedDate = b.PublishedDate,
                    IsLoaned = b.IsLoaned,
                    ImageUrl = b.ImageUrl,
                    GenreId = b.GenreId
                }).FirstOrDefaultAsync();

            if (book != null)
            {
                book.Genres = await AllGenresAsync();
            }

            return book;
		}

		public async Task<bool> HasLibrarianWithIdAsync(int bookid, string userid)
		{
            return await repository.AllReadOnly<Book>()
                .AnyAsync(b => b.Id == bookid && b.Librarian.User.Id == userid);
		}

		public async Task<bool> IsBookLoanedAsync(int bookId)
        {
			bool result = false;

			var book = await repository.GetByIdAsync<Book>(bookId);

			if (book != null)
			{
				result = book.IsLoaned == true;
			}

			return result ;
		}

		public async Task<bool> IsBookLoanedByMemberWithIdAsync(int bookId, int? memberId)
		{
            bool result = false;

            var book = await repository.GetByIdAsync<Book>(bookId);

            if (book != null)
            {
                result = book.LoanerId == memberId;
            }

            return result;
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

		public async Task LoanAsync(int bookId, int memberId)
		{
            var book = await repository.GetByIdAsync<Book>(bookId);

            if (book != null)
            {
                book.LoanerId = memberId;
                book.IsLoaned = true;

                await repository.SaveChangesAsync();
            }
		}

		public async Task ReturnAsync(int id)
		{
			var book = await repository.GetByIdAsync<Book>(id);

			if (book != null)
			{
				book.LoanerId = null;
				book.IsLoaned = false;

				await repository.SaveChangesAsync();
			}
		}
	}
}
