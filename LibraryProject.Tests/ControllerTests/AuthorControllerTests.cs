using LibraryProject.API.Controllers;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Services;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace LibraryProject.Tests.ControllerTests
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
            List<AuthorResponse> authors = new();

            authors.Add(new AuthorResponse
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            authors.Add(new AuthorResponse
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(authors);

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
            List<AuthorResponse> authors = new();

            _authorService
                .Setup(s => s.GetAllAuthors())
                .ReturnsAsync(authors);

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
            AuthorResponse author = new AuthorResponse
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _authorService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.GetById(1);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode200_WhenDataIsCreated()
        {
            // Arrange
            int authorId = 1;
            NewAuthor newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            AuthorResponse author = new AuthorResponse
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Create(It.IsAny<NewAuthor>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Create(newAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewAuthor newAuthor = new NewAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Create(It.IsAny<NewAuthor>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int authorId = 1;
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            AuthorResponse author = new AuthorResponse
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAuthor>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int authorId = 1;
            UpdateAuthor updateAuthor = new UpdateAuthor
            {
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _authorService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateAuthor>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(authorId, updateAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenAuthorIsDeleted()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int authorId = 1;

            _authorService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(authorId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
