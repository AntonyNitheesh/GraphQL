using GraphQLTest.Models;

namespace GraphQLTest.Services.Abstract
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBookById(int id);
        Task<Book> InsertBook(CreateBook book);
        Task<Book> UpdateBook(Book book);
        Task<bool> DeleteBookById(int id);
    }
}
