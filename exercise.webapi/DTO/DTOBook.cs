using exercise.webapi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace exercise.webapi.DTO
{
    public class DTOBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string  AuthorName { get; set; }

        public string Publisher { get; set; }
        [Column("publisherid")]
        public int PublisherId { get; set; }
        public List<DTOBook> Books { get; set; } 

    }
}
