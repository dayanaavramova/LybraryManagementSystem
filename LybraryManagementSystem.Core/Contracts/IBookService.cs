using LibraryManagementSystem.Core.Enumerations;
using LibraryManagementSystem.Core.Models.Book;
using LibraryManagementSystem.Core.Models.Home;

namespace LibraryManagementSystem.Core.Contracts
{
	public interface IBookService
    {
        Task<IEnumerable<BookIndexServiceModel>> LastThreeBooksAsync();
        Task<IEnumerable<AllBooksQueryModel>> AllBooksAsync();
        Task<IEnumerable<BookGenreServiceModel>> AllGenresAsync();
        Task<bool> GenreExistsAsync(int genreId);
        Task<int> CreateAsync(BookFormModel model, int librarianId);
        Task<BookQueryServiceModel> AllAsync(
            string? genre = null,
            BookSorting sorting = BookSorting.Newest,
            int currentpage = 1,
            int booksPerPage = 1);
        Task<IEnumerable<string>> AllGenresNamesAsync();

        Task<bool> ExistsAsync(int id);
        Task<BookDetailsServiceModel> BookDetailsByIdAsync(int id);
        Task EditAsync(int bookId, BookFormModel model);
        Task<bool> HasLibrarianWithIdAsync(int bookid, string userid);
        Task<BookFormModel?> GetBookFormModelByIdAsync(int id);
        Task<IEnumerable<BookServiceModel>> AllBooksByLibrarianIdAsync(int? librarianId);
		Task<IEnumerable<BookServiceModel>> AllBooksByMemberIdAsync(int? memberId);
		Task<bool> IsBookLoanedAsync(int bookId);
        Task<bool> IsBookLoanedByMemberWithIdAsync(int bookId, int? memberId);
        Task LoanAsync(int bookId, int memberId);
        Task ReturnAsync(int id);
        Task DeleteAsync(int id);
	}
}
