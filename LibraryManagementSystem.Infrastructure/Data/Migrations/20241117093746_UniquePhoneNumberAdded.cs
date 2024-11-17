using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    public partial class UniquePhoneNumberAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d2b55fdb-ac33-4576-b2b1-d2d088b0029e", "AQAAAAEAACcQAAAAEDn/JINigygeqBwacCxsy5IJr67yQBf4/KAJvlikrfwO7Z0U1/dVWVjdDougwiCUYg==", "facc48a7-c581-46f5-8ccf-64c805727e32" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65a42b0f-ae0f-4dc6-a03f-f228c100c5ea", "AQAAAAEAACcQAAAAEPDnRGat6bmOwBBAIIyfNnpWIyNm+D1KfS8c5Ydfa0wVpsw94BCaNpVQ4Zbb8xBi+w==", "37f908f8-8386-4b52-aeee-2a498a1aa296" });

            migrationBuilder.CreateIndex(
                name: "IX_Members_PhoneNumber",
                table: "Members",
                column: "PhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Librarians_PhoneNumber",
                table: "Librarians",
                column: "PhoneNumber",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Members_PhoneNumber",
                table: "Members");

            migrationBuilder.DropIndex(
                name: "IX_Librarians_PhoneNumber",
                table: "Librarians");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "58a572c6-be08-4648-8712-349e6dc4a7c9", "AQAAAAEAACcQAAAAEJIvD7KmLmcP6P/95pdqk5T4kMhWkjvGQEZs2GuVdImff65zi7IWzBWHO0lz+NhaQA==", "fe1bb83f-b301-405c-bbef-a4745f0e0707" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "40df5984-a425-4024-a608-fd75a87836ba", "AQAAAAEAACcQAAAAEDW38nlsuTla2AWaj9PczmHsgivmUX4RQhoeIbDyMih0HL4LCeZ4tDsx+nWVv+8URw==", "8240b448-d8c0-4d28-8766-37464cacf1f3" });
        }
    }
}
