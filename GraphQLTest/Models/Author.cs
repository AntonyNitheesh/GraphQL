namespace GraphQLTest.Models
{
    public class Author
    {
        public int id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
    }
    public class CreateAuthor
    {
        public string? name { get; set; }
        public string? email { get; set; }
    }
}
