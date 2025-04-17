namespace BlogPost.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        //Fk with Blog
        public int BlogId { get; set; }
        public Blog Blog { get; set; } = null!;

     
    }
}
