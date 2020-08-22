using ITAcademy.TaskTwo.Data.Models;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<Message> GetNoTrackingAsync(int id);
    }
}