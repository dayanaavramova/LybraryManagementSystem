using LibraryManagementSystem.Infrastructure.Data;
using LibraryManagementSystem.Infrastructure.Data.Common;
using LibraryManagementSystem.Infrastructure.Data.Models;
using LibraryManagementSystem.Tests.Mocks;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Tests.UnitTests
{
    public class UnitTestsBase
    {
        protected ApplicationDbContext data;
        protected Repository repository;

        [OneTimeSetUp]
        public void SetUpBase()
        {
            data = DatabaseMock.Instance;
            repository = new Repository(data);
            SeedDatabase();
        }

        public Librarian Librarian { get; private set; }
        public Member Member { get; private set; }
        public Book LoanedBook { get; private set; }

        private void SeedDatabase()
        {
            Librarian = new Librarian()
            {
                PhoneNumber = "+359278914583",
                User = new IdentityUser()
                {
                    Id = "LibrarianUserId",
                    Email = "lib@test.bg",
                    UserName = "Librarian"
                }
            };
            data.Librarians.Add(Librarian);

            Member = new Member()
            {
                PhoneNumber = "+359284017392",
                User = new IdentityUser()
                {
                    Id = "MemberUserId",
                    Email = "member@test.bg",
                    UserName = "Member"
                }
            };
            data.Members.Add(Member);

            LoanedBook = new Book()
            {
                Title = "First Test Book",
                Author = "First Test Author",
                ISBN = "1928372919833",
                ImageUrl = "https://marketplace.canva.com/EAFaQMYuZbo/1/0/1003w/canva-brown-rusty-mystery-novel-book-cover-hG1QhA7BiBU.jpg",
                PublishedDate = DateTime.Now,
                IsLoaned = true,
                Loaner = Member,
                Librarian = Librarian,
                Genre = new Genre() { Name = "Fantasy" }
            };
            data.Books.Add(LoanedBook);

            var book = new Book()
            {
                Title = "Second Test Book",
                Author = "Second Test Author",
                ISBN = "1928372919755",
                ImageUrl = "https://i.pinimg.com/736x/a1/f8/87/a1f88733921c820db477d054fe96afbb.jpg",
                PublishedDate = DateTime.Now,
                IsLoaned = false,
                Loaner = null,
                Librarian = Librarian,
                Genre = new Genre() { Name = "Sci-fi" }
            };
            data.Books.Add(book);
            data.SaveChanges();
        }

        [OneTimeTearDown]
        public void TearDownBase()
            => data.Dispose();
    }
}
