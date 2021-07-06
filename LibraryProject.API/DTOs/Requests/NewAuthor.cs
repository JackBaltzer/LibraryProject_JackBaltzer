using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTOs.Requests
{
    public class NewAuthor
    {
        [Required]
        [StringLength(32, ErrorMessage = "Firstname must be less than 32 chars")]
        [MinLength(1, ErrorMessage = "Firstname must contain atleast 1 char")]
        public string FirstName { get; set; }

        [StringLength(32, ErrorMessage = "MiddleName must be less than 32 chars")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "LastName must be less than 32 chars")]
        [MinLength(1, ErrorMessage = "LastName must contain atleast 1 char")]
        public string LastName { get; set; }
    }
}
