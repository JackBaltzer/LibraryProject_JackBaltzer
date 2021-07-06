using LibraryProject.API.Database;
using LibraryProject.API.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Repositories
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
    }

    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryProjectContext _context;

        public AuthorRepository(LibraryProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Author;
        }
    }
}
