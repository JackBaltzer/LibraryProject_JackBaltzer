using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.DTOs.Responses
{
    public class AuthorBookResponse
    {
        public int Id{ get; set; }
        public string Title { get; set; }
        public int Pages { get; set; }
    }
}
