using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Services;
using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests.UnitTests
{
    [TestFixture]
    public class BookServiceTests : UnitTestsBase
    {
        private IBookService bookService;

        [OneTimeSetUp]
        public void SetUp()
            => bookService = new BookService(repository);

        [Test]
        public async Task ExistsAsync_ReturnsTrue()
        {
            var result = await bookService.ExistsAsync(LoanedBook.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task HasLibrarianWithIdAsync_ReturnsTrue()
        {
            var result = await bookService.HasLibrarianWithIdAsync(LoanedBook.Id, LoanedBook.Librarian.UserId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsBookLoanedAsync_ReturnsTrue()
        {
            var result = await bookService.IsBookLoanedAsync(LoanedBook.Id);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task AllGenresNamesAsync_ReturnsCorrectResult()
        {
            var result = await bookService.AllGenresNamesAsync();

            var dbGenres = await repository.AllReadOnly<Genre>().ToListAsync();
            Assert.That(result.Count(), Is.EqualTo(dbGenres.Count()));

            var genreNames = dbGenres.Select(g => g.Name);
            Assert.That(genreNames.Contains(result.FirstOrDefault()));
        }

        [Test]
        public async Task AllBooksByLibrarianIdAsync_ShouldReturnCorrectBooks()
        {
            var libId = Librarian.Id;

            var result = await bookService.AllBooksByLibrarianIdAsync(libId);

            Assert.IsNotNull(result);

            var booksInDb = repository.AllReadOnly<Book>()
                .Where(b => b.LibrarianId == libId).ToList();
            Assert.That(result.Count(), Is.EqualTo(booksInDb.Count()));
        }

        [Test]
        public async Task AllBooksByMemberIdAsync_ShouldReturnCorrectBooks()
        {
            var memberId = Member.Id;

            var result = await bookService.AllBooksByMemberIdAsync(memberId);

            Assert.IsNotNull(result);

            var booksInDb = repository.AllReadOnly<Book>()
                .Where(b => b.LoanerId == memberId).ToList();
            Assert.That(result.Count(), Is.EqualTo(booksInDb.Count()));
        }

        [Test]
        public async Task BookDetailsByIdAsync_ReturnsCorrectBookData()
        {
            var bookId = LoanedBook.Id;

            var result = await bookService.BookDetailsByIdAsync(bookId);

            Assert.IsNotNull(result);

            var bookInDb = repository.AllReadOnly<Book>()
                .Where(b => b.Id == bookId)
                .FirstOrDefaultAsync();
            Assert.That(result.Id, Is.EqualTo(bookInDb.Id));
        }

        [Test]
        public async Task AllGenresAsync_ShouldReturnCorrectGenres()
        {
            var result = await bookService.AllGenresAsync();
            var dbGenres = repository.AllReadOnly<Genre>();

            Assert.That(result.Count(), Is.EqualTo(dbGenres.Count()));
            var genresNames = dbGenres.Select(g => g.Name);
            Assert.That(genresNames.Contains(result.FirstOrDefault().Name));
        }

        [Test]
        public async Task AllBooksAsync_ShouldReturnCorrectBooks()
        {
            var result = await bookService.AllBooksAsync();
            var dbBooks = repository.AllReadOnly<Book>();

            Assert.That(result.Count(), Is.EqualTo(dbBooks.Count()));
        }

        [Test]
        public async Task GenreExistsAsync_ReturnsTrue()
        {
            var genreId = LoanedBook.GenreId;
            var result = await bookService.GenreExistsAsync(genreId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task IsBookLoanedByMemberWithIdAsync_ReturnsTrue()
        {
            var bookId = LoanedBook.Id;
            var memberId = LoanedBook.LoanerId;

            var result = await bookService.IsBookLoanedByMemberWithIdAsync(bookId, memberId);
            Assert.IsTrue(result);
        }
    }
}
