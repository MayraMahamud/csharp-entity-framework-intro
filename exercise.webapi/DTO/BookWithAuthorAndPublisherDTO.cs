namespace exercise.webapi.DTO
{
    public class BookWithAuthorAndPublisherDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DTOAuthor Author { get; set; }
        public DTOPublisher Publisher { get; set; }
    }
}
