using TaskTwo.Data.Models;

namespace TaskTwo.Data.Interfaces
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        int GetPhoneIdByNumber(string phoneNumber);
    }
}