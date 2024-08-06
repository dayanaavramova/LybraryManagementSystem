using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", 0, "aeb5f6ae-f2a1-4788-9bcb-db62f96d10bd", "member@mail.com", false, false, null, "member@mail.com", "member@mail.com", "AQAAAAEAACcQAAAAEAH4wzJKQ3L7Dec7k9DHraC1Jic0fmcnGQGYq71Y/gNGlCCJWDRgrskzradw8yyFKQ==", null, false, "95b91b1c-9c3b-4a86-9882-225f11c2be2e", false, "member@mail.com" },
                    { "dea12856-c198-4129-b3f3-b893d8395082", 0, "ef8f3527-b497-4c34-89e9-21317bb7a571", "librarian@mail.com", false, false, null, "librarian@mail.com", "librarian@mail.com", "AQAAAAEAACcQAAAAEDdjwAq538gM25XguBqyDydE2XLdj6qVfD0rPOXqT1/dsP5AH1nBZCcQVT1yoV1oFA==", null, false, "d1f921ec-7b5e-4b99-a286-fb4e39bd8a02", false, "librarian@mail.com" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fantasy" },
                    { 2, "Sci-fi" },
                    { 3, "Romance" },
                    { 4, "Thriller" }
                });

            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "PhoneNumber", "UserId" },
                values: new object[] { 1, "+359888888888", "dea12856-c198-4129-b3f3-b893d8395082" });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "MembershipDate", "PhoneNumber", "UserId" },
                values: new object[] { 1, new DateTime(2025, 8, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "+359888888888", "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "GenreId", "ISBN", "ImageUrl", "LibrarianId", "PublishedDate", "Title" },
                values: new object[] { 1, "Leigh Bardugo", 3, 1, "1830173902204", "https://books.google.bg/books/publisher/content?id=yhIRBwAAQBAJ&hl=bg&pg=PP1&img=1&zoom=3&sig=ACfU3U1UBl05osmdBHZtH4PylujGV0zAFw&w=1280", 1, new DateTime(2015, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Six of Crows" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "GenreId", "ISBN", "ImageUrl", "LibrarianId", "PublishedDate", "Title" },
                values: new object[] { 2, "Douglas Adams", 5, 2, "0174927459174", "https://encrypted-tbn2.gstatic.com/images?q=tbn:ANd9GcTBZT4WfTCYmJLT8wqhEIaq87cO3rolDE_DWbp7ayWG5Y9TO2u_", 1, new DateTime(1980, 10, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Hitchhiker's Guide to the Galaxy" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "GenreId", "ISBN", "ImageUrl", "LibrarianId", "PublishedDate", "Title" },
                values: new object[] { 3, "Casey McQuiston", 2, 3, "5819367426382", "https://encrypted-tbn3.gstatic.com/images?q=tbn:ANd9GcRmtxGyRnwNGSJGZ7nxBHdY8jfCJx3jdJDX9IKYile0ch7KY07G", 1, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Red, White & Royal Blue" });

            migrationBuilder.InsertData(
                table: "Loans",
                columns: new[] { "Id", "BookId", "MemberId", "ReturnDate" },
                values: new object[] { 1, 1, 1, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Loans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e");

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082");
        }
    }
}
