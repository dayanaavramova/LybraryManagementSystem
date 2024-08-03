using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    public partial class LibrarianBookConnection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibrarianId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Books_LibrarianId",
                table: "Books",
                column: "LibrarianId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Librarians_LibrarianId",
                table: "Books",
                column: "LibrarianId",
                principalTable: "Librarians",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Librarians_LibrarianId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_LibrarianId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "LibrarianId",
                table: "Books");
        }
    }
}
