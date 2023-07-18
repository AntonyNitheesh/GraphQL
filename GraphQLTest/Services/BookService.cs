using GraphQL;
using GraphQLTest.Data;
using GraphQLTest.Models.Response;
using GraphQLTest.Models;
using GraphQLTest.Services.Abstract;

namespace GraphQLTest.Services
{
    public class BookService : GraphqlClient, IBookService
    {
        public async Task<IEnumerable<Book>> GetBooks()
        {
            var getBooksQuery = @"query books {
                                        Books(order_by: {id: asc}){
                                            id
                                            authorId
                                            bookName
                                        }
                                       }";

            var graphQlRequest = new GraphQLRequest()
            {
                Query = getBooksQuery
            };
            var response = await _graphQLHttpClient.SendQueryAsync<BooksResponse>(graphQlRequest);
            return response.Data.Books;
        }
        public async Task<bool> DeleteBookById(int id)
        {
            var deleteEmployeeMutation = @"mutation deleteBook($id: Int) {
                                          delete_Book(where: {id: {_eq: $id}}) {
                                            affected_rows
                                          }
                                        }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = deleteEmployeeMutation,
                Variables = new
                {
                    id
                }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<bool>(graphQlRequest);
            return true;
        }

        public async Task<Book> GetBookById(int id)
        {
            var getByIdQuery = @"query BookById($id: Int!) {
                                  Books_by_pk(id: $id) {
                                    id
                                    authorId
                                    bookName
                                  }
                                }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = getByIdQuery,
                Variables = new { id }
            };

            var response = await _graphQLHttpClient.SendQueryAsync<BookResponse>(graphQlRequest);
            return response.Data.Book;
        }


        public async Task<Book> InsertBook(CreateBook Book)
        {
            var insertEmployeeMutation = @"mutation insertBook($Book: Book_insert_input!) {
                                                      insert_Book_one(object: $Book) {
                                                        id
                                                        authorId
                                                        bookName
                                                      }
                                                    }";

            var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new { Book = Book }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<BookResponse>(graphQlRequest);

            return response.Data.Book;
        }

        public async Task<Book> UpdateBook(Book Book)
        {
            var employeeUpdateMutation = @"mutation updateBook($id:Int!, $Book: Book_set_input) {
                                              update_Book_by_pk(pk_columns: {id: $id}, _set: $Book) {
                                                id
                                              }
                                            }";
            var variables = new
            {
                id = Book.id,
                Book = new CreateBook
                {
                    authorId = Book.authorId,
                    bookName = Book.bookName
                }
            };

            var graphqlRequest = new GraphQLRequest
            {
                Query = employeeUpdateMutation,
                Variables = variables
            };

            var response = await _graphQLHttpClient.SendMutationAsync<BookResponse>(graphqlRequest);

            return response.Data.Book;
        }
    }
}
