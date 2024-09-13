using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class AuthorRepository : IAuthorRepository
    {
        DataContext _db;

        public AuthorRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _db.Authors.Include(a => a.Books).ToListAsync();

        }
        public async Task<Author> GetAuthorById(int id)
        {
            return await _db.Authors.FirstAsync(a => a.Id == id);   
            //var entity = await _db.Authors.FirstOrDefaultAsync(b => b.Id == id);
            //return entity;
           
            //return await _db.Authors.Include(a => a.Books).FirstOrDefaultAsync(a=> a.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }



    }
}
