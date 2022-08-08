using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTwo.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TaskTwo.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T>
        where T : class
    {
        public BaseRepository(IApplicationContext context)
        {
            Db = context;
        }

        public IApplicationContext Db { get; set; }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Db.Set<T>().ToList();
        }
        
        public virtual async Task<T> GetAsync(int id)
        {
            return await Db.Set<T>().FindAsync(id);
        }

        public virtual async Task CreateAsync(T item)
        {
            await Db.Set<T>().AddAsync(item);
        }
   
        public virtual void Update(T item)
        {
            Db.Attach(item);
            Db.Entry(item).State = EntityState.Modified;
        }

        public virtual async Task DeleteAsync(int id)
        {
            Db.Set<T>().Remove(await Db.Set<T>().FindAsync(id));
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(string lookFor)
        {
            return await Db.Set<T>().ToListAsync();
        } 
    }
}