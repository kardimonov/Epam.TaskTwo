using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;

namespace TaskTwo.Logic.Interfaces
{
    public interface ISubjectDecorator : IRepository<Subject>, IBaseDecorator
    {
    }
}