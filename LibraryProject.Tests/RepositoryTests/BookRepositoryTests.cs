using LibraryProject.API.Database;
using LibraryProject.API.Database.Entities;
using LibraryProject.API.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace LibraryProject.Tests.RepositoryTests
{
    public class BookRepositoryTests
    {
        private DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly BookRepository _sut;

        public BookRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProjectBooks")
                .Options;

            _context = new LibraryProjectContext(_options);

            _sut = new BookRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            // we need an author we can reference from the book.
            _context.Author.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            _context.Book.Add(new Book
            {
                Id = 1,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1,
            });

            _context.Book.Add(new Book
            {
                Id = 2,
                Title = "A Clash of Kings",
                Pages = 708,
                AuthorId = 1,
            });
            await _context.SaveChangesAsync();
            int expectedSize = 2;

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.Equal(expectedSize, result.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfBooks_WhenNoBookExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Book>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_SholdReturnTheBook_IfBookExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            // we need an author we can reference from the book.
            _context.Author.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            int bookId = 1;

            _context.Book.Add(new Book
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1,
            });

            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_IfBookDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int bookId = 1;

            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToBook_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            int expectedId = 1;

            Book book = new Book
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1,
            };

            // Act
            var result = await _sut.Create(book);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async Task Create_ShouldFailToAddBook_WhenAddingBookWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            Book book = new Book
            {
                Id = 1,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1,
            };
            _context.Book.Add(book);
            await _context.SaveChangesAsync();

            // Act
            Func<Task> action = async () => await _sut.Create(book);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnBook_WhenBookExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            // we need an author we can reference from the book.
            _context.Author.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            int bookId = 1;
            _context.Book.Add(new Book
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1
            });

            await _context.SaveChangesAsync();

            Book updateBook = new Book
            {
                Id = bookId,
                Title = "A Clash of Kings",
                Pages = 708,
                AuthorId = 1
            };

            // Act
            var result = await _sut.Update(bookId, updateBook);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Book>(result);
            Assert.Equal(bookId, result.Id);
            Assert.Equal(updateBook.Title, result.Title);
            Assert.Equal(updateBook.Pages, result.Pages);
        }
    }
}
