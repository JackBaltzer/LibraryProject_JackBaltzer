using LibraryProject.API.Controllers;
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
    }
}
