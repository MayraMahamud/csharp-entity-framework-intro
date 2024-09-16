using exercise.webapi.Models;

namespace exercise.webapi.DTO
{
    public class BookWithAuthorDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public DTOAuthor Author { get; set; }
        
        
        

        

    }
}
