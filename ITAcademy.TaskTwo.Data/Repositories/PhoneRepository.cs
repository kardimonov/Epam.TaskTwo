using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using System.Linq;

namespace ITAcademy.TaskTwo.Data.Repositories
{
    public class PhoneRepository : BaseRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(IApplicationContext context)
            : base(context)
        {            
        }

        public int GetPhoneIdByNumber(string phoneNumber) =>
            Db.Phones
            .FirstOrDefault(p => p.Number == phoneNumber)
            .Id;        
    }
}