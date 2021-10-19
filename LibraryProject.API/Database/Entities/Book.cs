using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryProject.API.Database.Entities
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(32)")]
        public string Title { get; set; }

        [Required]
        public int Pages { get; set; }

        [ForeignKey("Author.Id")]
        public int AuthorId { get; set; }

        public Author Author { get; set; } 

        public string Image { get; set; }
    }
}
