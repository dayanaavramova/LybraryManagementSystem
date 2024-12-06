using LibraryManagementSystem.Core.Contracts;
using LibraryManagementSystem.Core.Services;
using LibraryManagementSystem.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests.UnitTests
{
    [TestFixture]
    public class LibrarianServiceTests : UnitTestsBase
    {
        private ILibrarianService librarianService;

        [OneTimeSetUp]
        public void SetUp()
            => librarianService = new LibrarianService(repository);

        [Test]
        public async Task GetLibrarianIdAsync_ShouldReturnCorrectUserid()
        {
            var resultId = librarianService.GetLibrarianIdAsync(Librarian.UserId);
            Assert.That(Convert.ToInt32(resultId), Is.EqualTo(Librarian.Id));
        }

        [Test]
        public async Task ExistsByIdAsync_ShouldReturnTrue()
        {
            var result = await librarianService.ExistsByIdAsync(Librarian.UserId);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task UserWithPhoneNumberExistsAsync_ShouldReturnTrue()
        {
            var result = await librarianService.UserWithPhoneNumberExistsAsync(Librarian.PhoneNumber);
            Assert.IsTrue(result);
        }

        [Test]
        public async Task CreateAsync_ShouldWorkCorrectly()
        {
            var librariansCountBefore = repository.AllReadOnly<Librarian>().Count();

            await librarianService.CreateAsync(Librarian.UserId, Librarian.PhoneNumber);

            var librariansCountAfter = repository.AllReadOnly<Librarian>().Count();
            Assert.That(librariansCountAfter, Is.EqualTo(librariansCountBefore + 1));

            var newLibId = await librarianService.GetLibrarianIdAsync(Librarian.UserId);
            var newLibinDb = await repository.GetByIdAsync<Librarian>(newLibId);
            Assert.IsNotNull(newLibinDb);
            Assert.That(newLibinDb.UserId, Is.EqualTo(Librarian.UserId));
            Assert.That(newLibinDb.PhoneNumber, Is.EqualTo(Librarian.PhoneNumber));
        }

        [Test]
        public async Task UserHasRoleAsync_ShouldReturnFalse()
        {
            var result = await librarianService.UserHasRoleAsync(Librarian.UserId);
            Assert.IsFalse(result);
        }
    }
}
