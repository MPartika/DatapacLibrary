using DatapacLibrary.Domain;
using DatapacLibrary.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatapacLibrary.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PopulateDb : Migration
    {
        private readonly LibraryDbContext _dbContext;

        public PopulateDb()
        {
            _dbContext = new LibraryDbContext();
        }

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _dbContext.AddRange(AddUsers());
            _dbContext.AddRange(AddBooks());

            _dbContext.SaveChanges();
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM USERS", true);
            migrationBuilder.Sql("DELETE FROM BOOKS", true);
        }

        private IEnumerable<User> AddUsers()
        {
            for (int i = 1; i <= 10; i++)
            {
                yield return new User
                {
                    Name = $"User{i}",
                    Email = $"user{i}@example.com",
                    Password = AuthenticationHelper.HashPassword($"Password{i}", out byte[] salt),
                    Salt = salt
                };
            }
        }

        private IEnumerable<Book> AddBooks()
        {
            var rnd = new Random();
            for (int i = 1; i <= 50; i++)
            {
                yield return new Book
                {
                    Title = $"Title{i}",
                    Author = $"Author{i}",
                    ISBN = $"ISBN{i}",
                    Publisher = $"Publisher{i}",
                    PublicationYear = 1970 + i,
                    NumberOfCopies = rnd.Next(1, 6)
                };
            }
        }
    }
}
