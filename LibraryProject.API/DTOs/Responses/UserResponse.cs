using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.DTOs.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Role Role { get; set; }
    }
}
