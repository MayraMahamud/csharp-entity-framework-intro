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
        [ForeignKey("Author")]  
        public int AuthorId { get; set; }
        [Column("author")]
        public Author Author { get; set; }
       
        public Publisher Publisher { get; set; }    
        
       
        [Column("publisherid")]
        [ForeignKey("Publisher")]

        public int PublisherId { get; set; }    
    }
}
