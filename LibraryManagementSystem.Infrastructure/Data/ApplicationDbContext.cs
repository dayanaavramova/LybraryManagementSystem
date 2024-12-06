using LibraryManagementSystem.Infrastructure.Data.Models;
using LibraryManagementSystem.Infrastructure.Data.SeedDb;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace LibraryManagementSystem.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        private bool _seedDb;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, bool seedDb = true)
            : base(options)
        {
            if (Database.IsRelational())
            {
                Database.Migrate();
            }
            else
            {
                Database.EnsureCreated();
            }
            _seedDb = seedDb;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (_seedDb)
            {
                builder.ApplyConfiguration(new UserConfiguration());
                builder.ApplyConfiguration(new LibrarianConfiguration());
                builder.ApplyConfiguration(new MemberConfiguration());
                builder.ApplyConfiguration(new GenreConfiguration());
                builder.ApplyConfiguration(new BookConfiguration());
                builder.ApplyConfiguration(new ReservationConfiguration());
                builder.ApplyConfiguration(new ReviewConfiguration());
            }

            base.OnModelCreating(builder);
        }

        public DbSet<Book> Books { get; set; } = null!;
        public DbSet<Genre> Genres { get; set; } = null!;
        public DbSet<Librarian> Librarians { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Member> Members { get; set; } = null!;
        public DbSet<Reservation> Reservations { get; set; } = null!;
    }
}