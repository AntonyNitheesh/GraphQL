using Newtonsoft.Json;

namespace GraphQLTest.Models.Response
{
    public class BooksResponse
    {
        [JsonProperty("book")]
        public IEnumerable<Book>? Books { get; set; }
    }

    public class BookResponse
    {
        [JsonProperty("book_by_pk")]
        public Book? Book { get; set; }
    }
}
