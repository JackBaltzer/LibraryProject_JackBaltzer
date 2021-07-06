using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using LibraryProject.API.Services;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests
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
            List<Author> Authors = new List<Author>();
            Authors.Add(new Author
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            Authors.Add(new Author
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });

            _authorRepository
                .Setup(a => a.GetAll())
                .ReturnsAsync(Authors);

            // Act
            var result = await _sut.GetAllAuthors();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<AuthorResponse>>(result);
        }
    }
}
