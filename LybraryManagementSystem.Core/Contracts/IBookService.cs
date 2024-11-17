using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Core.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookIndexServiceModel>> LastThreeBooksAsync();
        Task<IEnumerable<AllBooksQueryModel>> AllBooksAsync();
        Task<IEnumerable<BookGenreServiceModel>> AllGenresAsync();
        Task<bool> GenreExistsAsync(int genreId);
        Task<int> CreateAsync(BookFormModel model, int librarianId);
    }
}
