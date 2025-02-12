using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThuVien.Models;

namespace QuanLyThuVien.Controllers
{
    public class BookController : Controller
    {

        private readonly ApplicationDBContext dbContext;

        public List<Book> Book;

        public BookController(ApplicationDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        //Hiển thị vs Tìm kiếm
        public async Task<IActionResult> Index(string id)
        {
            if (id == null)
            {
                List<Book> books = await dbContext.Books.ToListAsync();
                return View(books);
            }
            else
            {
                var qr = from b in dbContext.Books
                         orderby b.BookId descending
                         select b;
                var BooksFiltered = await qr.Where(b => b.Title.Contains(id)).ToListAsync();
                return View(BooksFiltered);
            }
        }

        public async Task<IActionResult> Listbook(string id)
        {

            if (id == null)
            {

                return View();
            }
            else
            {
                int _id = int.Parse(id);
                List<Book> books = await dbContext.Books
                              .Where(b => b.AuthorId == _id)
                              .ToListAsync();

                return View(books);
            }
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> Update(int Id)
        {
            var books = await dbContext.Books.FindAsync(Id);
            return View(books);
        }
        [HttpPost]
        public async Task<IActionResult> Update(Book Model)
        {
            var Sach = await dbContext.Books.FindAsync(Model.BookId);
            if (Sach is not null)
            {
                Sach.BookId = Model.BookId;
                Sach.Title = Model.Title;
                Sach.PublicationDate = Model.PublicationDate;
                Sach.PublishHouse = Model.PublishHouse;
                Sach.AuthorId = Model.AuthorId;


                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Book");
        }

        public IActionResult Delete(int id)
        {
            var sach = dbContext.Books.
                Where(x => x.BookId == x.BookId).FirstOrDefault();
            {
                dbContext.Books.Remove(sach);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index", "Book");

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Add(Book Model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"Error: {error.ErrorMessage}");
                }
                return Json(new { success = false, message = "Lỗi dữ liệu đầu vào." });
            }

            var authorExists = await dbContext.Authors.AnyAsync(a => a.AuthorId == Model.AuthorId);
            if (!authorExists)
            {
                return Json(new { success = false, message = "Tác giả không tồn tại." });
            }

            var sach = new Book
            {
                Title = Model.Title,
                PublicationDate = Model.PublicationDate,
                PublishHouse = Model.PublishHouse,
                AuthorId = Model.AuthorId
            };

            await dbContext.Books.AddAsync(sach);
            await dbContext.SaveChangesAsync();

            return Json(new { success = true, redirectUrl = Url.Action("Index") });
        }


    }
}