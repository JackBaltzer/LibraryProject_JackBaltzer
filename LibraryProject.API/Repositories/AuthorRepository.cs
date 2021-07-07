using LibraryProject.API.Database;
using LibraryProject.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAll();
        Task<Author> GetById(int authorId);
        Task<Author> Create(Author author);
        Task<Author> Update(int authorId, Author author);
        Task<Author> Delete(int authorId);
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;

        public AuthorRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAll()
        {
            return await _context.Author.ToListAsync();
        }

        public async Task<Author> GetById(int authorId)
        {
            return await _context.Author.FirstOrDefaultAsync(a => a.Id == authorId);
        }

        public async Task<Author> Create(Author author)
        {
            _context.Author.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> Update(int authorId, Author author)
        {
            Author updateAuthor = await _context.Author.FirstOrDefaultAsync(a => a.Id == authorId);
            if (updateAuthor != null)
            {
                updateAuthor.FirstName = author.FirstName;
                updateAuthor.LastName = author.LastName;
                updateAuthor.MiddleName = author.MiddleName;
                await _context.SaveChangesAsync();
            }
            return updateAuthor;
        }

        public async Task<Author> Delete(int authorId)
        {
            Author author = await _context.Author.FirstOrDefaultAsync(a => a.Id == authorId);
            if (author != null)
            {
                _context.Author.Remove(author);
                await _context.SaveChangesAsync();
            }
            return author;
        }
    }
}
