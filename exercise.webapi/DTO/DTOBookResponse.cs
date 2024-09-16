using exercise.webapi.Models;

namespace exercise.webapi.DTO
{
    public class DTOBookResponse
    {
       
        public List<DTOBook> Books { get; set; } = new List<DTOBook>();
    }

}
