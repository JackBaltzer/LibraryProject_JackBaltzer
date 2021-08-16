using LibraryProject.API.Database.Entities;
using LibraryProject.API.Helpers;

namespace LibraryProject.API.DTOs.Responses
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }
    }
}
