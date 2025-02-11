using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace QuanLyThuVien.ViewModels
{
	public class LoginVM
	{
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Display(Name = "Remeber Me")]
        public bool RemeberMe { get; set; }
    }
}

