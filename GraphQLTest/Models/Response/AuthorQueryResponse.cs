using Newtonsoft.Json;

namespace GraphQLTest.Models.Response
{
    public class AuthorsResponse
    {
        [JsonProperty("author")]
        public IEnumerable<Author>? Authors { get; set; }
    }

    public class AuthorResponse
    {
        [JsonProperty("author_by_pk")]
        public Author? Author { get; set; }
    }
}
