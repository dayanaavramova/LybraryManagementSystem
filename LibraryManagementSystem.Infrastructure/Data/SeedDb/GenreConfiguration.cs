using LibraryManagementSystem.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagementSystem.Infrastructure.Data.SeedDb
{
    internal class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            var data = new SeedData();
            builder.HasData(new Genre[] { data.FantasyGenre, data.ScifiGenre, data.RomanceGenre, data.ThrillerGenre });
        }
    }
}
