using LibraryProject.API.Controllers;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests
{
    public class AuthorControllerTests
    {
        private readonly AuthorController _sut;
        private readonly Mock<IAuthorService> _authorService = new();

        public AuthorControllerTests()
        {
            _sut = new AuthorController(_authorService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<AuthorResponse> Authors = new();

            Authors.Add(new AuthorResponse
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            Authors.Add(new AuthorResponse
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode204_WhenNoElementsExists()
        {
            // Arrange
            List<AuthorResponse> Authors = new();

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(Authors);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenNullIsReturnedFromService()
        {
            // Arrange
            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetAll();

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
 
        [Fact]
        public async void GetById_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            int authorId = 1;
            AuthorResponse Author = new AuthorResponse
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };
        
            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(Author);

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
    }
}
