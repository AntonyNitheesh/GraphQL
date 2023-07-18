using GraphQLTest.Models;
using GraphQLTest.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLTest.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public async Task<IActionResult> GetBooks()
        {
            var books = await _bookService.GetBooks();
            return View(books);
        }

        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        public ActionResult InsertBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertBook(Book book)
        {
            try
            {
                var newbook = new CreateBook
                {
                    authorId = book.authorId,
                    bookName = book.bookName
                };
                await _bookService.InsertBook(newbook);
                return RedirectToAction("GetBooks");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> UpdateBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBook(int id, Book book)
        {
            try
            {
                var newbook = new Book
                {
                    id = book.id,
                    authorId = book.authorId,
                    bookName = book.bookName
                };
                await _bookService.UpdateBook(newbook);
                return RedirectToAction("GetBooks");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteBook(int id)
        {
            var book = await _bookService.GetBookById(id);
            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBook(int id, IFormCollection collection)
        {
            try
            {
                await _bookService.DeleteBookById(id);
                return RedirectToAction("GetBooks");
            }
            catch
            {
                return View();
            }
        }
    }
}
