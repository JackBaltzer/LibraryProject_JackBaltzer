using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.ServiceTests
{
    public class AuthorServiceTests
    {
        private readonly AuthorService _sut;
        private readonly Mock<IAuthorRepository> _authorRepository = new();

        public AuthorServiceTests()
        {
            _sut = new AuthorService(_authorRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfAuthorResponses_WhenAuthorsExist()
        {
            // Arrange
            List<Author> authors = new();
            authors.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            authors.Add(new Author
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });

            _authorRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(authors);

            // Act
            var result = await _sut.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfAuthorResponses_WhenNoAuthorsExists()
        {
            // Arrange
            List<Author> authors = new();

            _authorRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(authors);

            // Act
            var result = await _sut.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<AuthorResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnAnAuthorResponse_WhenAuthorExists()
        {
            // Arrange
            int authorId = 1;

            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(author.Id, result.Id);
            Assert.Equal(author.FirstName, result.FirstName);
            Assert.Equal(author.LastName, result.LastName);
            Assert.Equal(author.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _authorRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Create_ShouldReturnAuthorResponse_WhenCreateIsSuccess()
        {
            // Arrange
            NewAuthor newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            int authorId = 1;

            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorRepository
                .Setup(a => a.Create(It.IsAny<Author>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Create(newAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(newAuthor.FirstName, result.FirstName);
            Assert.Equal(newAuthor.LastName, result.LastName);
            Assert.Equal(newAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void Update_ShouldReturnUpdatedAuthorResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            int authorId = 1;

            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<AuthorResponse>(result);
            Assert.Equal(authorId, result.Id);
            Assert.Equal(updateAuthor.FirstName, result.FirstName);
            Assert.Equal(updateAuthor.LastName, result.LastName);
            Assert.Equal(updateAuthor.MiddleName, result.MiddleName);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenAuthorDoesNotExist()
        {
            // Arrange
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            int authorId = 1;

            _authorRepository
                .Setup(a => a.Update(It.IsAny<int>(), It.IsAny<Author>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int authorId = 1;

            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorRepository
                .Setup(a => a.Delete(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            Assert.True(result);
        }
    }
}
