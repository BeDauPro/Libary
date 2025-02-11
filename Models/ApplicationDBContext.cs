using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace QuanLyThuVien.Models
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
		public ApplicationDBContext()
		{
		}
		public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

		public DbSet<Book> Books { get; set; }
		public DbSet<Author> Authors { get; set; }
    }
}

