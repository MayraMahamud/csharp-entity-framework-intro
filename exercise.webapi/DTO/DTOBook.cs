using System.Reflection;

namespace exercise.webapi.DTO
{
    public class DTOBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public List<DTOBook> books { get; set; } /*= new List<DTOBook>();*/

    }
}
