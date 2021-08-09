using LibraryProject.API.Database;
using LibraryProject.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAll();
        Task<Book> GetById(int bookId);
        Task<Book> Create(Book book);
        Task<Book> Update(int bookId, Book book);
        Task<Book> Delete(int bookId);
    }

    public class BookRepository : IBookRepository
    {
        private readonly LibraryProjectContext _context;

        public BookRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _context.Book
                .Include(b => b.Author)
                .ToListAsync();
        }

        public async Task<Book> GetById(int bookId)
        {
            return await _context.Book
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<Book> Create(Book book)
        {
            _context.Book.Add(book);
            await _context.SaveChangesAsync();
            return book;
        }

        public async Task<Book> Update(int bookId, Book book)
        {
            Book updateBook = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (updateBook!= null)
            {
                updateBook.Title= book.Title;
                updateBook.Pages= book.Pages;
                updateBook.AuthorId = book.AuthorId;
                await _context.SaveChangesAsync();
            }
            return updateBook;
        }

        public async Task<Book> Delete(int bookId)
        {
            Book book = await _context.Book
                .FirstOrDefaultAsync(b => b.Id == bookId);

            if (book != null)
            {
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();
            }
            return book;
        }
    }
}
