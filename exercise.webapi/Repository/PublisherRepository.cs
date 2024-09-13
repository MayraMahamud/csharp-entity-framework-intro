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

        public async Task<IEnumerable<Publisher>> GetPublishers()
        {
            return await _db.Publishers.Include(p => p.Books).ToListAsync();

        }

        public async Task<Publisher> GetAPublisher(int id)
        {
            var entity = await _db.Publishers.FirstOrDefaultAsync(p => p.Id == id);
            return entity;
        }
    }
}