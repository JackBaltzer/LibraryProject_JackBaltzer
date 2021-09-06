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
    public class BookServiceTests
    {
        private readonly BookService _sut;
        private readonly Mock<IBookRepository> _bookRepository = new();
        private readonly Mock<IAuthorRepository> _authorRepository = new();

        public BookServiceTests()
        {
            _sut = new BookService(_bookRepository.Object, _authorRepository.Object);
        }

        [Fact]
        public async void GetAll_ShouldReturnListOfBookResponses_WhenBooksExists()
        {
            // Arrange
            List<Book> books = new();
            books.Add(new Book
            {
                Id = 1,
                Title = "A Game of Thrones",
                Pages = 694,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Martin",
                    MiddleName = "R.R."
                }
            });
            books.Add(new Book
            {
                Id = 2,
                Title = "A Clash of Kings",
                Pages = 708,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Martin",
                    MiddleName = "R.R."
                }
            });

            _bookRepository
                .Setup(b => b.GetAll())
                .ReturnsAsync(books);

            // Act
            var result = await _sut.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.IsType<List<BookResponse>>(result);

        }

        [Fact]
        public async void GetAll_ShouldReturnEmptyListOfBookResponses_WhenNoBookExists()
        {
            // Arrange
            List<Book> books = new();

            _bookRepository
               .Setup(b => b.GetAll())
               .ReturnsAsync(books);

            // Act
            var result = await _sut.GetAllBooks();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
            Assert.IsType<List<BookResponse>>(result);
        }

        [Fact]
        public async void GetById_ShouldReturnABookResponse_WhenBookExists()
        {
            // Arrange
            int bookId = 1;
            Book book = new Book
            {
                Id = 1,
                Title = "A Game of Thrones",
                Pages = 694,
                Author = new Author
                {
                    Id = 1,
                    FirstName = "George",
                    LastName = "Martin",
                    MiddleName = "R.R."
                }
            };

            _bookRepository
                .Setup(b => b.GetById(It.IsAny<int>()))
                .ReturnsAsync(book);

            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(book.Id, result.Id);
            Assert.Equal(book.Title, result.Title);
            Assert.Equal(book.Pages, result.Pages);
            Assert.Equal(book.Author.Id, result.Author.Id);


        }

        [Fact]
        public async void GetById_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;

            _bookRepository
                .Setup(b => b.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            // Act
            var result = await _sut.GetById(bookId);

            // Assert
            Assert.Null(result);

        }

        [Fact]
        public async void Create_ShouldReturnBookResponse_WhenCreateIsSuccess()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;

            NewBook newBook = new NewBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };


            Book book = new Book
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };

            Author author = new Author
            {
                Id = authorId,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            };

            _bookRepository
                .Setup(b => b.Create(It.IsAny<Book>()))
                .ReturnsAsync(book);

            _authorRepository
                .Setup(a => a.GetById(It.IsAny<int>()))
                .ReturnsAsync(author);

            // Act
            var result = await _sut.Create(newBook);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public async void Update_ShouldReturnUpdatedBookResponse_WhenUpdateIsSuccess()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;

            UpdateBook updateBook = new UpdateBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };

            Book book = new Book
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };

            _bookRepository
                .Setup(b => b.Update(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(book);

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
            var result = await _sut.Update(bookId, updateBook);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BookResponse>(result);
            Assert.Equal(bookId, result.Id);
        }

        [Fact]
        public async void Update_ShouldReturnNull_WhenBookDoesNotExist()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;

            UpdateBook updateBook = new UpdateBook
            {
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };

            _bookRepository
               .Setup(b => b.Update(It.IsAny<int>(), It.IsAny<Book>()))
               .ReturnsAsync(() => null);


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
            var result = await _sut.Update(bookId, updateBook);
            
            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async void Delete_ShouldReturnTrue_WhenDeleteIsSuccess()
        {
            // Arrange
            int bookId = 1;
            int authorId = 1;

            Book book = new Book
            {
                Id = bookId,
                Title = "A Game of Thrones",
                Pages = 694,
                AuthorId = authorId
            };

            _bookRepository
                .Setup(b => b.Delete(It.IsAny<int>()))
                .ReturnsAsync(book);


            // Act
            var result = await _sut.Delete(bookId);

            // Assert
            Assert.True(result);

        }
    }
}
