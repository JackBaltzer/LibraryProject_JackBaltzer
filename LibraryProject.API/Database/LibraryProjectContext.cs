using LibraryProject.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.API.Database
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Martin",
                    MiddleName = "R.R."
                },
                new Author
                {
                    Id = 2,
                    FirstName = "James",
                    LastName = "Corey",
                    MiddleName = "S.A."
                });

            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "A Game of Thrones",
                    Pages = 694,
                    AuthorId = 1,
                },
                new Book
                {
                    Id = 2,
                    Title = "A Clash of Kings",
                    Pages = 708,
                    AuthorId = 1,
                },
                new Book
                {
                    Id = 3,
                    Title = "Leviathan Wakes",
                    Pages = 577,
                    AuthorId = 2,
                }, new Book
                {
                    Id = 4,
                    Title = "Babylons Ashes",
                    Pages = 544,
                    AuthorId = 2,
                }
            );
        }
    }
}
