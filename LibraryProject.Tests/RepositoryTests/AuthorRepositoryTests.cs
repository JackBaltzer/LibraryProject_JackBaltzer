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
    public class AuthorRepositoryTests
    {
        private DbContextOptions<LibraryProjectContext> _options;
        private readonly LibraryProjectContext _context;
        private readonly AuthorRepository _sut;

        public AuthorRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<LibraryProjectContext>()
                .UseInMemoryDatabase(databaseName: "LibraryProject")
                .Options;

            _context = new LibraryProjectContext(_options);

            _sut = new AuthorRepository(_context);
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfAuthors_WhenAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            _context.Author.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });
            _context.Author.Add(new Author
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });
            await _context.SaveChangesAsync();
            int expectedSize = 2;

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Equal(expectedSize, result.Count);
        }

        [Fact]
        public async Task GetAll_ShouldReturnEmptyListOfAuthors_WhenNoAuthorsExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            // Act
            var result = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<List<Author>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetById_ShouldReturnTheAuthor_IfAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            _context.Author.Add(new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
        }

        [Fact]
        public async Task GetById_ShouldReturnNull_IfAuthorDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Create_ShouldAddIdToAuthor_WhenSavingToDatabase()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int expectedId = 1;
            Author author = new Author
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            // act
            var result = await _sut.Create(author);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(expectedId, result.Id);
        }
         
        [Fact]
        public async Task Create_ShouldFailToAddAuthor_WhenAddingAuthorWithExistingId()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();

            Author author = new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            // act
            Func<Task> action = async () => await _sut.Create(author);

            // Assert
            var ex = await Assert.ThrowsAsync<ArgumentException>(action);
            Assert.Contains("An item with the same key has already been added", ex.Message);
        }

        [Fact]
        public async Task Update_ShouldChangeValuesOnAuthor_WhenAuthorExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            Author updateAuthor = new Author
            {
                Id = authorId,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            };

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async Task Update_ShouldReturnNull_WhenAuthorDoesNotExists()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author updateAuthor = new Author
            {
                Id = authorId,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            };

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Delete_ShouldReturnDeletedAuthor_WhenAuthorIsDeleted()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;
            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };
            _context.Author.Add(author);
            await _context.SaveChangesAsync();

            var result = await _sut.Delete(authorId);
            var authors = await _sut.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Author>(result);
            Assert.Equal(authorId, result.Id);

            Assert.Empty(authors);
        }

        [Fact]
        public async Task Delete_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            await _context.Database.EnsureDeletedAsync();
            int authorId = 1;

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            Assert.Null(result);
        }
    }
}