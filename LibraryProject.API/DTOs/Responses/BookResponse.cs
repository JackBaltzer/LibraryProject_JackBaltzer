using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.DTOs.Responses
{
    public class BookResponse
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
        public BookAuthorResponse Author { get; set; }
    }
}
