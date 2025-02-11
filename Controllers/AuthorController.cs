using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDBContext dbContext;

        public AuthorController(ApplicationDBContext dBContext)
        {
            this.dbContext = dBContext;
        }

        public List<Author> Author { get; private set; }

        public async Task<IActionResult>Index(string id)
        {
            if (id == null)
            {
                List<Author> Authors = await dbContext.Authors.ToListAsync();
                return View(Authors);
            }
            else
            {
                var qr = from a in dbContext.Authors
                         orderby a.AuthorId descending
                         select a;
                if (!string.IsNullOrEmpty(id))
                {
                    var Authors = qr.Where(a => a.Name.Contains(id)).ToList();
                    return View(Authors);
                }
                else
                {
                    Author = await qr.ToListAsync();
                    return View();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var Authors = await dbContext.Authors.FindAsync(Id);
            return View(Authors);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Author Model)
        {
            var TacGia = await dbContext.Authors.FindAsync(Model.AuthorId);
            if (TacGia is not null)
            {
                TacGia.AuthorId = Model.AuthorId;
                TacGia.Name = Model.Name;
                TacGia.BirthDate = Model.BirthDate;


                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Author");
        }

        public IActionResult Delete(int id)
        {
            var tacgia = dbContext.Authors.
                Where(x => x.AuthorId == x.AuthorId).FirstOrDefault();
            {
                dbContext.Authors.Remove(tacgia);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Author");

        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Add(Author Model)
        {
            if (ModelState.IsValid)
            {
                var tacgia = new Author
                {
                    AuthorId = Model.AuthorId,
                    Name = Model.Name,
                    BirthDate = Model.BirthDate
                };
                await dbContext.Authors.AddAsync(tacgia);
                await dbContext.SaveChangesAsync();

                return Json(new { success = true, redirectUrl = Url.Action("Index") });
            
            }
            return Json(new { success = false, message = "Đã có lỗi xảy ra khi thêm tác giả." });
          
        }
    
    }
}

