using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyThuVien.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime? PublicationDate { get; set; }

        [Required]
        [StringLength(50)]
        public string PublishHouse { get; set; }

        public int AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }
    }
}
