using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.API.Database.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string MiddleName { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}
