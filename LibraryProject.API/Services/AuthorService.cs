using LibraryProject.API.Database.Entities;
using LibraryProject.API.DTOs.Requests;
using LibraryProject.API.DTOs.Responses;
using LibraryProject.API.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IAuthorService
    {
        Task<List<AuthorResponse>> GetAllAuthors();
        Task<AuthorResponse> GetById(int authorId);
        Task<AuthorResponse> Create(NewAuthor newAuthor);
        Task<AuthorResponse> Update(int authorId, UpdateAuthor updateAuthor);
        Task<bool> Delete(int authorId);
    }

    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorResponse>> GetAllAuthors()
        {
            List<Author> authors = await _authorRepository.GetAll();
            return authors == null ? null : authors.Select(a => new AuthorResponse
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName
            }).ToList();
        }

        public async Task<AuthorResponse> GetById(int authorId)
        {
            Author author = await _authorRepository.GetById(authorId);

            return author == null ? null : new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName
            };
        }

        public async Task<AuthorResponse> Create(NewAuthor newAuthor)
        {
            Author author = new Author
            {
                FirstName = newAuthor.FirstName,
                MiddleName = newAuthor.MiddleName,
                LastName = newAuthor.LastName
            };

            author = await _authorRepository.Create(author);

            return author == null ? null : new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName
            };
        }

        public async Task<AuthorResponse> Update(int authorId, UpdateAuthor updateAuthor)
        {
            Author author = new Author
            {
                FirstName = updateAuthor.FirstName,
                MiddleName = updateAuthor.MiddleName,
                LastName = updateAuthor.LastName
            };

            author = await _authorRepository.Update(authorId, author);

            return author == null ? null : new AuthorResponse
            {
                Id = author.Id,
                FirstName = author.FirstName,
                MiddleName = author.MiddleName,
                LastName = author.LastName
            };
        }

        public async Task<bool> Delete(int authorId)
        {
            var result = await _authorRepository.Delete(authorId);
            return true;
        }
    }
}
