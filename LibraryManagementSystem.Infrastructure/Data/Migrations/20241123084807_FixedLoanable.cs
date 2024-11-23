using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagementSystem.Infrastructure.Migrations
{
    public partial class FixedLoanable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CopiesAvailable",
                table: "Books");

            migrationBuilder.AddColumn<bool>(
                name: "IsLoaned",
                table: "Books",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ff403ed8-ee10-4951-8ef5-eadf0ebb0628", "AQAAAAEAACcQAAAAENgXbT3pWEWa4KscxU4HHUWmyFtWOPwWIAvMKDW/adrn9YUrjP2NvfXElC40v7SY3w==", "53191652-5cdb-417c-871b-daf8159d57c4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a16a91e-1fe9-4ece-baaf-18becff70898", "AQAAAAEAACcQAAAAEJYx6UbCeUokzsnAQoelyya3+vRrvDw19/nmxWF8FRNnlY1kduK0pJe4RKjHklQ7Rg==", "312da7a6-79aa-4d6b-bd9a-048c4f781805" });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsLoaned",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsLoaned",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "CopiesAvailable",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "CopiesAvailable",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "CopiesAvailable",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3,
                column: "CopiesAvailable",
                value: 2);
        }
    }
}
