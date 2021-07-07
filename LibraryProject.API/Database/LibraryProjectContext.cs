using LibraryProject.API.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryProject.API.Database
{
    public class LibraryProjectContext : DbContext
    {
        public LibraryProjectContext() { }
        public LibraryProjectContext(DbContextOptions<LibraryProjectContext> options) : base(options) { }

        public DbSet<Author> Author { get; set; }

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
        }
    }
}
