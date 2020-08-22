using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        IEnumerable<T> GetAll();

        Task CreateAsync(T item);

        Task<T> GetAsync(int id);

        void Update(T item);

        Task DeleteAsync(int id);

        Task<IEnumerable<T>> SearchAsync(string searchString);
    }
}