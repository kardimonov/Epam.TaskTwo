using ITAcademy.TaskTwo.Data.Models;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IPhoneRepository : IRepository<Phone>
    {
        int GetPhoneIdByNumber(string phoneNumber);
    }
}