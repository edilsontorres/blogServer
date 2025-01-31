
namespace blog_BackEnd.Entites
{
    public class Post
    {
        public int Id {get; set;}
        public string? Slug {get; set;}
        public string? Title {get; set;}
        public string? Content {get; set;}
        public string? Author{get;set;}
        public string? CoverImg { get; set; }
        public DateTime CreatedAt {get; set;} = DateTime.Now;
        public DateTime LastDateUpdate {get; set;} = DateTime.Now;
        
    }
}