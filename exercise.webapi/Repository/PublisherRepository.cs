using exercise.webapi.Data;
using exercise.webapi.Models;
using Microsoft.EntityFrameworkCore;

namespace exercise.webapi.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        DataContext _db;

        public PublisherRepository(DataContext db)
        {
            _db = db;
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _db.Publishers.Include(p => p.Books).ThenInclude(b => b.Author).ToListAsync();

        }

        public async Task<Publisher> GetAPublisher(int id)
        {
            return await _db.Publishers.Include(p => p.Books).ThenInclude(b=> b.Author).FirstOrDefaultAsync( p => p.Id == id);   
          
        }


       




    }
}