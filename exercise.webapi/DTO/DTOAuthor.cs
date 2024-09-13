namespace exercise.webapi.DTO
{
    public class DTOAuthor
    {
        public string name {  get; set; }   
        public List<DTOBook> books { get; set; } = new List<DTOBook>(); 
    }
}
