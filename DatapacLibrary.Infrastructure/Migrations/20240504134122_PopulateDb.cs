using DatapacLibrary.Domain;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatapacLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            AddAdmin(migrationBuilder);
            AddUsers(migrationBuilder);
            AddBooks(migrationBuilder);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM ADMINS", true);
            migrationBuilder.Sql("DELETE FROM USERS", true);
            migrationBuilder.Sql("DELETE FROM BOOKS", true);
        }

        private void AddAdmin(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 5; i++)
            {
                migrationBuilder.InsertData(
                    table: "Admins", 
                    columns: ["Name", "Password", "Salt", "Created", "Updated"], 
                    values: [$"admin{i}", AuthenticationHelper.HashPassword($"Password{i}", out byte[] salt), salt, DateTime.UtcNow, DateTime.UtcNow]);

            }
        }

        private void AddUsers(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 10; i++)
            {
                migrationBuilder.InsertData(
                    table: "Users", 
                    columns: ["Name", "Email", "Created", "Updated"], 
                    values: [$"User{i}", $"user{i}@example.com", DateTime.UtcNow, DateTime.UtcNow]);

            }
        }

        private void AddBooks(MigrationBuilder migrationBuilder)
        {
            var rnd = new Random();
            for (int i = 1; i <= 50; i++)
            {
                migrationBuilder.InsertData(
                    table: "Books",
                    columns: ["Title", "Author", "Publisher", "PublicationYear", "ISBN", "Created", "Updated"],
                    values: [$"Title{i}", $"Author{i}", $"Publisher{i}", 1970 + i, $"ISBN{i}", DateTime.UtcNow, DateTime.UtcNow]);
            }
        }
    }
}
