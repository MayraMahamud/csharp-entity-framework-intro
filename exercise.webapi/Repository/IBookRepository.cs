using exercise.webapi.Models;

namespace exercise.webapi.Repository
{
    public interface IBookRepository
    {
        public Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> GetABook(int id);
        Task<Book> GetById(int id);
        Task<Book> UpdateBook( Book book);
        Task<Book> CreateBook(Book book);
        Task<Book> DeleteBook(Book book);
        Task SaveChangesAsync();
    }

}