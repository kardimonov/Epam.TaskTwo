using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskTwo.Data.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(IApplicationContext context)
            : base(context)
        {
        }

        public override async Task<IEnumerable<Message>> GetAllAsync() =>
            await Db.Messages
            .Include(m => m.Addressee)
            .OrderBy(m => m.DispatchResult)
            .ThenByDescending(m => m.TimeCreated)
            .Select(m => m)
            .ToListAsync();

        public async Task<Message> GetNoTrackingAsync(int id) =>
            await Db.Messages
            .AsNoTracking()           
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}