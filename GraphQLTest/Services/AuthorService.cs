using GraphQL.Client.Http;
using GraphQL;
using GraphQLTest.Services.Abstract;
using GraphQLTest.Data;
using GraphQLTest.Models.Response;
using GraphQLTest.Models;
using System.Xml.Linq;

namespace GraphQLTest.Services
{
    public class AuthorService : GraphqlClient, IAuthorService 
    {
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var getAuthorsQuery = @"query authors {
                                        Author (order_by: {id: asc}){
                                            id
                                            name
                                            email
                                        }
                                       }";

            var graphQlRequest = new GraphQLRequest()
            {
                Query = getAuthorsQuery
            };
            var response = await _graphQLHttpClient.SendQueryAsync<AuthorsResponse>(graphQlRequest);
            return response.Data.Authors;
        }
        public async Task<bool> DeleteAuthorById(int id)
        {
            var deleteEmployeeMutation = @"mutation deleteAuthor($id: Int) {
                                          delete_Author(where: {id: {_eq: $id}}) {
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

        public async Task<Author> GetAuthorById(int id)
        {
            var getByIdQuery = @"query authorById($id: Int!) {
                                  Author_by_pk(id: $id) {
                                    id
                                    name
                                    email
                                  }
                                }";
            var graphQlRequest = new GraphQLRequest
            {
                Query = getByIdQuery,
                Variables = new { id }
            };

            var response = await _graphQLHttpClient.SendQueryAsync<AuthorResponse>(graphQlRequest);
            return response.Data.Author;
        }


        public async Task<Author> InsertAuthor(CreateAuthor author)
        {
            var insertEmployeeMutation = @"mutation insertAuthor($author: Author_insert_input!) {
                                                      insert_Author_one(object: $author) {
                                                        id
                                                        name
                                                        email
                                                      }
                                                    }";

            var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new { author = author }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<AuthorResponse>(graphQlRequest);

            return response.Data.Author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            var employeeUpdateMutation = @"mutation updateAuthor($id:Int!, $author: Author_set_input) {
                                              update_Author_by_pk(pk_columns: {id: $id}, _set: $author) {
                                                id
                                              }
                                            }";
            var variables = new
            {
                id = author.id,
                author = new CreateAuthor
                {
                    name = author.name,
                    email = author.email
                }
            };

            var graphqlRequest = new GraphQLRequest
            {
                Query = employeeUpdateMutation,
                Variables = variables
            };

            var response = await _graphQLHttpClient.SendMutationAsync<AuthorResponse>(graphqlRequest);

            return response.Data.Author;
        }
        public async Task<Author> InsertAuthorandBook(CreateAuthor author, CreateBook book)
        {
        var insertEmployeeMutation = @"mutation createAuthorAndBook($author: [Author_insert_input!]!, $book: [Books_insert_input!]!) {
                                        insert_Author(objects: $author) {
                                            returning {
                                                id
                                                name
                                                email
                                            }
                                        }
                                        insert_Books(objects: $book) {
                                            returning {
                                                id
                                                bookName
                                                author {
                                                    id
                                                    name
                                                    email
                                                }
                                            }
                                        }
                                    }";

            var graphQlRequest = new GraphQLRequest
            {
                Query = insertEmployeeMutation,
                Variables = new { author = author, book = book }
            };

            var response = await _graphQLHttpClient.SendMutationAsync<AuthorResponse>(graphQlRequest);

            return response.Data.Author;
        }
    }
}
