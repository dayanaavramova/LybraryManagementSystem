using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryManagementSystem.Infrastructure.Data.SeedDb
{
    internal class SeedData
    {
        public IdentityUser LibrarianUser { get; set; }
        public IdentityUser MemberUser { get; set; }
        public Librarian Librarian { get; set; }
        public Member Member { get; set; }
        public Genre FantasyGenre { get; set; }
        public Genre ScifiGenre { get; set; }
        public Genre RomanceGenre { get; set; }
        public Genre ThrillerGenre { get; set; }
        public Book FirstBook { get; set; }
        public Book SecondBook { get; set; }
        public Book ThirdBook { get; set; }
        public Loan FirstLoan { get; set; }
        public Reservation FirstReservation { get; set; }

        public SeedData()
        {
            SeedUsers();
            SeedMember();
            SeedLibrarian();
            SeedGenre();
            SeedBooks();
            SeedLoans();
            SeedReservations();
        }

        private void SeedUsers()
        {
            var hasher = new PasswordHasher<IdentityUser>();

            LibrarianUser = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "librarian@mail.com",
                NormalizedUserName = "librarian@mail.com",
                Email = "librarian@mail.com",
                NormalizedEmail = "librarian@mail.com"
            };

            LibrarianUser.PasswordHash =
                 hasher.HashPassword(LibrarianUser, "librarian123");

            MemberUser = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "member@mail.com",
                NormalizedUserName = "member@mail.com",
                Email = "member@mail.com",
                NormalizedEmail = "member@mail.com"
            };

            MemberUser.PasswordHash =
            hasher.HashPassword(MemberUser, "member123");
        }

        private void SeedLibrarian()
        {
            Librarian = new Librarian()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                UserId = LibrarianUser.Id
            };
        }

        private void SeedMember()
        {
            Member = new Member()
            {
                Id = 1,
                PhoneNumber = "+359888888899",
                MembershipDate = new DateTime(2025, 8, 23),
                UserId = MemberUser.Id
            };
        }

        private void SeedGenre()
        {
            FantasyGenre = new Genre()
            {
                Id = 1,
                Name = "Fantasy"
            };

            ScifiGenre = new Genre()
            {
                Id = 2,
                Name = "Sci-fi"
            };

            RomanceGenre = new Genre()
            {
                Id = 3,
                Name = "Romance"
            };

            ThrillerGenre = new Genre()
            {
                Id = 4,
                Name = "Thriller"
            };
        }

        private void SeedBooks()
        {
            FirstBook = new Book()
            {
                Id = 1,
                Title = "Six of Crows",
                Author = "Leigh Bardugo",
                ISBN = "1830173902204",
                PublishedDate = new DateTime(2015, 9, 29),
                IsLoaned = true,
                ImageUrl = "https://books.google.bg/books/publisher/content?id=yhIRBwAAQBAJ&hl=bg&pg=PP1&img=1&zoom=3&sig=ACfU3U1UBl05osmdBHZtH4PylujGV0zAFw&w=1280",
                GenreId = FantasyGenre.Id,
                LibrarianId = Librarian.Id
            };

            SecondBook = new Book()
            {
                Id = 2,
                Title = "The Hitchhiker's Guide to the Galaxy",
                Author = "Douglas Adams",
                ISBN = "0174927459174",
                PublishedDate = new DateTime(1980, 10,12),
				IsLoaned = false,
                ImageUrl = "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTBZT4WfTCYmJLT8wqhEIaq87cO3rolDE_DWbp7ayWG5Y9TO2u_",
                GenreId = ScifiGenre.Id,
                LibrarianId = Librarian.Id
            };

            ThirdBook = new Book()
            {
                Id = 3,
                Title = "Red, White & Royal Blue",
                Author = "Casey McQuiston",
                ISBN = "5819367426382",
                PublishedDate = new DateTime(2019, 5, 14),
				IsLoaned = false,
                ImageUrl = "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRmtxGyRnwNGSJGZ7nxBHdY8jfCJx3jdJDX9IKYile0ch7KY07G",
                GenreId = RomanceGenre.Id,
                LibrarianId = Librarian.Id
            };
        }

        private void SeedLoans()
        {
            FirstLoan = new Loan()
            {
                Id = 1,
                BookId = FirstBook.Id,
                MemberId = Member.Id,
                ReturnDate = new DateTime(2025, 3, 10)
            };
        }

        private void SeedReservations()
        {
            FirstReservation = new Reservation()
            {
                Id = 1,
                BookId = FirstBook.Id,
                MemberId = Member.Id
            };
        }
    }
}
