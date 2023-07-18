using GraphQLTest.Models;
using GraphQLTest.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQLTest.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        public AuthorController(IAuthorService authorService)
        {
            _authorService= authorService;
        }

        public async Task<IActionResult> GetAuthors()
        {
            var authors =await _authorService.GetAuthors();
            return View(authors);
        }

        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        public ActionResult InsertAuthor()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertAuthor(Author author)
        {
            try
            {
                var newauthor = new CreateAuthor
                {
                    name = author.name,
                    email = author.email
                };
                await _authorService.InsertAuthor(newauthor);
                return RedirectToAction("GetAuthors");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> UpdateAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateAuthor(int id, Author author)
        {
            try
            {
                var newauthor = new Author
                {
                    id = author.id,
                    name = author.name,
                    email = author.email
                };
                await _authorService.UpdateAuthor(newauthor);
                return RedirectToAction("GetAuthors");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> DeleteAuthor(int id)
        {
            var author = await _authorService.GetAuthorById(id);
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAuthor(int id, IFormCollection collection)
        {
            try
            {
                await _authorService.DeleteAuthorById(id);
                return RedirectToAction("GetAuthors");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult InsertAuthorandBook()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> InsertAuthorandBook(Author author, Book book)
        {
            try
            {
                var newauthor = new CreateAuthor
                {
                    name = author.name,
                    email = author.email
                };
                var newbook = new CreateBook
                {
                    authorId = author.id,
                    bookName = "Data Science"
                };
                await _authorService.InsertAuthorandBook(newauthor, newbook);
                return RedirectToAction("GetAuthors");
            }
            catch
            {
                return View();
            }
        }
    }
}
