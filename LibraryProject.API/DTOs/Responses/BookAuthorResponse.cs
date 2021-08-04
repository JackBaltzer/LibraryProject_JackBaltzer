using System.Collections.Generic;

namespace LibraryProject.API.DTOs.Responses
{
    public class BookAuthorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
