using LibraryProject.API.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Services
{
    public interface IAuthorService
    {
        List<AuthorResponse> GetAllAuthors();
    }

    public class AuthorService : IAuthorService
    {
        public List<AuthorResponse> GetAllAuthors()
        {
            List<AuthorResponse> Authors = new();

            Authors.Add(new AuthorResponse
            {
                Id = 1,
                FirstName = "George",
                LastName = "Martin",
                MiddleName = "R.R."
            });

            Authors.Add(new AuthorResponse
            {
                Id = 2,
                FirstName = "James",
                LastName = "Corey",
                MiddleName = "S.A."
            });

            return Authors;
        }
    }
}
