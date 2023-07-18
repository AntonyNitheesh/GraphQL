using GraphQLTest.Models;

namespace GraphQLTest.Services.Abstract
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> InsertAuthor(CreateAuthor author);
        Task<Author> UpdateAuthor(Author author);
        Task<bool> DeleteAuthorById(int id);
        Task<Author> InsertAuthorandBook(CreateAuthor author, CreateBook book);
    }
}
