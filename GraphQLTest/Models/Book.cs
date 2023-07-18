namespace GraphQLTest.Models
{
    public class Book
    {
        public int id { get; set; }
        public int authorId { get; set; }
        public string? bookName { get; set; }
    }
    public class CreateBook
    {
        public int authorId { get; set; }
        public string? bookName { get; set; }
    }
}
