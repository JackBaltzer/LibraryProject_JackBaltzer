using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace LibraryProject.API.Services
{
    public interface IAuthorService
    {
        List<AuthorResponse> GetAllAuthors();
    }

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public List<AuthorResponse> GetAllAuthors()
        {
            IEnumerable<Author> Authors = _authorRepository.GetAll();
            return Authors.Select(a => new AuthorResponse
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName
            }).ToList();
        }
    }
}
