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
    public class BookControllerTests
    {
        private readonly BookController _sut;
        private readonly Mock<IBookService> _bookService = new();

        public BookControllerTests()
        {
            _sut = new BookController(_bookService.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnStatusCode200_WhenDataExists()
        {
            // Arrange
            List<BookResponse> books = new();

            books.Add(new BookResponse
            {
                Id = 1,
                Pages = 120,
                Author = new()
            });

            books.Add(new BookResponse
            {
                Id = 2,
                Pages = 220,
                Author = new()
            });

            _bookService
                .Setup(s => s.GetAllBooks())
                .ReturnsAsync(books);

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
            List<BookResponse> books = new();

            _bookService
                .Setup(s => s.GetAllBooks())
                .ReturnsAsync(books);

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
            _bookService
                .Setup(s => s.GetAllBooks())
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
            _bookService
                .Setup(s => s.GetAllBooks())
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
            int bookId = 1;
            BookResponse book = new BookResponse
            {
                Id = bookId,
                Pages = 123,
                Author = new()
            };

            _bookService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(book);

            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode404_WhenAuthorDoesNotExist()
        {
            // Arrange
            int bookId = 1;

            _bookService
                .Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);
            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(404, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void GetById_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            _bookService
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
            int bookId = 1;
            NewBook newBook = new NewBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1
            };

            BookResponse book = new BookResponse
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                Author = new()
            };

            _bookService
                .Setup(s => s.Create(It.IsAny<NewBook>()))
                .ReturnsAsync(book);

            // Act
            var result = await _sut.Create(newBook);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Create_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            NewBook newBook = new NewBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1
            };

            _bookService
                .Setup(s => s.Create(It.IsAny<NewBook>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Create(newBook);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode200_WhenDataIsSaved()
        {
            // Arrange
            int bookId = 1;
            UpdateBook updateBook = new UpdateBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1
            };

            BookResponse book = new BookResponse
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                Author = new()
            };

            _bookService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateBook>()))
                .ReturnsAsync(book);

            // Act
            var result = await _sut.Update(bookId, updateBook);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Update_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int bookId = 1;
            UpdateBook updateAuthor = new UpdateBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = 1
            };

            _bookService
                .Setup(s => s.Update(It.IsAny<int>(), It.IsAny<UpdateBook>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Update(bookId, updateAuthor);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode204_WhenAuthorIsDeleted()
        {
            // Arrange
            int bookId = 1;

            _bookService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);

            // Act
            var result = await _sut.Delete(bookId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(204, statusCodeResult.StatusCode);
        }

        [Fact]
        public async void Delete_ShouldReturnStatusCode500_WhenExceptionIsRaised()
        {
            // Arrange
            int bookId = 1;

            _bookService
                .Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(() => throw new System.Exception("This is an exception"));

            // Act
            var result = await _sut.Delete(bookId);

            // Assert
            var statusCodeResult = (IStatusCodeActionResult)result;
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
    }
}
