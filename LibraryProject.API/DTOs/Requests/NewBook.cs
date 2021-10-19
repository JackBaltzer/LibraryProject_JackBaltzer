using System.ComponentModel.DataAnnotations;

namespace LibraryProject.API.DTOs.Requests
{
    public class NewBook
    {
        [Required]
        [StringLength(32, ErrorMessage = "Title must be less than 32 chars")]
        public string Title { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Pages must be between 1 and 2147483647")]
        public int Pages { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "AuthorId must be between 1 and 2147483647")]
        public int AuthorId { get; set; }

        public string  Image { get; set; }
    }
}
