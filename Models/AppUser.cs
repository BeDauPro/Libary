using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace QuanLyThuVien.Models
{
	public class AppUser: IdentityUser
    {
        [StringLength(100)]
        [MaxLength(100)]
        [Required]
        public string? Name { set; get; }
        public string? Address { set; get; }
    }
}

