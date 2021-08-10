using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IBookService
    {
        Task<List<BookResponse>> GetAllBooks();
        Task<BookResponse> GetById(int bookId);
        Task<BookResponse> Create(NewBook newBook);
        Task<BookResponse> Update(int bookId, UpdateBook updateBook);
        Task<bool> Delete(int bookId);
    }

    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<List<BookResponse>> GetAllBooks()
        {
            List<Book> books = await _bookRepository.GetAll();

            return books == null ? null : books.Select(b => new BookResponse
            {
                Id = b.Id,
                Title = b.Title,
                Pages = b.Pages,
                Author = new BookAuthorResponse
                {
                    Id = b.Author.Id,
                    FirstName = b.Author.FirstName,
                    LastName = b.Author.LastName,
                    MiddleName = b.Author.MiddleName
                }
            }).ToList();
        }

        public async Task<BookResponse> GetById(int bookId)
        {
            Book books = await _bookRepository.GetById(bookId);

            return books == null ? null : new BookResponse
            {
                Id = books.Id,
                Title = books.Title,
                Pages = books.Pages,
                Author = new BookAuthorResponse
                {
                    Id = books.Author.Id,
                    FirstName = books.Author.FirstName,
                    LastName = books.Author.LastName,
                    MiddleName = books.Author.MiddleName
                }
            };
        }

        public async Task<BookResponse> Create(NewBook newBook)
        {
            Book book = new Book
            {
                Title = newBook.Title,
                Pages = newBook.Pages,
                AuthorId = newBook.AuthorId
            };

            book = await _bookRepository.Create(book);
            Author author = await _authorRepository.GetById(book.AuthorId);

            return book == null ? null : new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Pages = book.Pages,
                Author = new BookAuthorResponse
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName,
                    MiddleName = author.MiddleName
                }
            };
        }

        public async Task<BookResponse> Update(int bookId, UpdateBook updateBook)
        {
            Book book = new Book
            {
                Title = updateBook.Title,
                Pages = updateBook.Pages,
                AuthorId = updateBook.AuthorId
            };

            book = await _bookRepository.Update(bookId, book);
            Author author = await _authorRepository.GetById(book.AuthorId);

            return book == null ? null : new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Pages = book.Pages,
                Author = new BookAuthorResponse
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    MiddleName = author.MiddleName,
                    LastName = author.LastName
                }
            };
        }

        public async Task<bool> Delete(int bookId)
        {
            var result = await _bookRepository.Delete(bookId);

            return result != null;
        }
    }
}
