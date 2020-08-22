using System.Collections.Generic;
using System.Threading.Tasks;
using ITAcademy.TaskTwo.Data.Interfaces;

namespace ITAcademy.TaskTwo.Logic.Decorators
{
    public abstract class BaseDecorator<T> : IRepository<T>
        where T : class
    {
        private readonly IUnitOfWork unit;
        private readonly IRepository<T> repo;

        public BaseDecorator(IRepository<T> repository, IUnitOfWork unitOfWork)
        {
            unit = unitOfWork;
            repo = repository;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        { 
            return await repo.GetAllAsync();
        }            

        public virtual IEnumerable<T> GetAll()
        {
            return repo.GetAll();
        }

        public async virtual Task CreateAsync(T item)
        {
            await repo.CreateAsync(item);
            NotifyWhenModified();
            unit.Save();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await repo.GetAsync(id);
        }

        public virtual void Update(T item)
        {
            repo.Update(item);
            NotifyWhenModified();
            unit.Save();
        }

        public virtual async Task DeleteAsync(int id)
        {
            await repo.DeleteAsync(id);
            NotifyWhenModified();
            unit.Save();
        }

        public virtual async Task<IEnumerable<T>> SearchAsync(string searchString)
        {
            return await repo.SearchAsync(searchString);
        } 

        protected abstract void NotifyWhenModified();
    }
}