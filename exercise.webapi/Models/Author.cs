using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace exercise.webapi.Models
{
    [Table("author")]
    public class Author
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("firstname")]
        public string FirstName { get; set; }
        [Column("lastname")]
        public string LastName { get; set; }
        [Column("email")]
        public string Email { get; set; }

        //[JsonIgnore] // Todo: replace this with DTO approach
        public ICollection<Book> Books { get; set; } = new List<Book>();

    }
}
