﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace QuanLyThuVien.ViewModels
{
	public class Register
	{
        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Passwords don't match")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
    }
}

