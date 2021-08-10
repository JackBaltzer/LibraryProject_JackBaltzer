using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTOs.Requests
{
    public class UpdateAuthor
    {
        [Required]
        [StringLength(32, ErrorMessage = "Firstname must be less than 32 chars")]
        public string FirstName { get; set; }

        [StringLength(32, ErrorMessage = "MiddleName must be less than 32 chars")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "LastName must be less than 32 chars")]
        public string LastName { get; set; }
    }
}
