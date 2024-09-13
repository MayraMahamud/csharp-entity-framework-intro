using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class BookRepository : IBookRepository
    {
        DataContext _db;

        public BookRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await _db.Books.Include(b => b.Author).ToListAsync();

        }

        public async Task<Book> GetABook(int id)
        {
            var entity = await _db.Books.FirstOrDefaultAsync(b => b.Id == id);
            return entity;
        }

        //public async Task<Book> GetById(int id)
        //{
        //    return await (_db.Books.Include(b => b.Author).FirstOrDefaultAsync());

        //}

        public async Task<Book> UpdateBook(Book book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
            return book;


        }

        public async Task<Book> CreateBook(Book book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return new Book { Id = book.Id, AuthorId = book.AuthorId };
        }

        public async Task<Book> DeleteBook(Book book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return book;

        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<Book> GetById( int id)
        {
            return await _db.Books.FirstAsync(a => a.Id == id);



        }

        
    }
}
