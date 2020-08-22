using ITAcademy.TaskTwo.Data.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITAcademy.TaskTwo.Logic.Interfaces
{
    public interface IMessageService : IBaseService
    {
        Task<IEnumerable<Message>> GetAllAsync();

        Task<Message> GetAsync(int id);

        Task<Message> GetWithAddresseeAsync(int id);

        Task<string> HandleMessageAsync(Message message, string method);
    }
}