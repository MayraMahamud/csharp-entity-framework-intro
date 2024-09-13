using System.ComponentModel.DataAnnotations.Schema;

namespace exercise.webapi.Models
{
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("authorid")]
        public int? AuthorId { get; set; }
        [Column("author")]
        public Author Author { get; set; }
       
        
        [Column("publisher")]
        public Publisher Publisher { get; set; }
        [Column("publisherid")]
        public int PublisherId { get; set; }    
    }
}
