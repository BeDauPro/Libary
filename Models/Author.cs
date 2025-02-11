using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    [Table("Author")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime? BirthDate { get; set; }
    }
}

