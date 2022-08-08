using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace TaskTwo.Data.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IApplicationContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetAllItemsWithAllListsAsync() =>
            await Db.Employees
            .Include(em => em.Phones)
            .Include(em => em.Assignments)
            .ThenInclude(es => es.Subject)
            .Include(em => em.Appointments)
            .ThenInclude(ep => ep.Position)
            .OrderBy(em => em.SurName)
            .ThenBy(em => em.FirstName)
            .ThenBy(em => em.SecondName)
            .ThenBy(em => em.Email)
            .ThenByDescending(em => em.Birth)
            .Select(em => em)
            .ToListAsync();

        public async Task<Employee> GetItemWithAllListsAsync(int id) =>
            await Db.Employees
            .Include(em => em.Phones)
            .Include(em => em.Assignments)
            .ThenInclude(es => es.Subject)
            .Include(em => em.Appointments)
            .ThenInclude(ep => ep.Position)
            .FirstOrDefaultAsync(em => em.Id == id);

        public async Task<IEnumerable<Employee>> GetEmployeesWithPhonesAsync() =>
            await Db.Employees
            .Include(em => em.Phones)
            .ToListAsync();

        public override IEnumerable<Employee> GetAll() =>
            Db.Employees
            .Include(em => em.Phones)
            .ToList();

        public override async Task<Employee> GetAsync(int id) =>
            await Db.Employees
            .Include(em => em.Phones)
            .FirstOrDefaultAsync(e => e.Id == id);

        public override async Task<IEnumerable<Employee>> SearchAsync(string searchString) =>
            await Db.Employees
            .Include(e => e.Phones)
            .Where(e =>
            e.SurName.Contains(searchString) ||
            e.FirstName.Contains(searchString) ||
            e.SecondName.Contains(searchString) ||
            e.Email.Contains(searchString) ||
            e.Birth.ToString().Contains(searchString) ||
            e.Phones.Any(p => p.Number.Contains(searchString)))
            .ToListAsync();

        public async Task<Employee> GetByPrimaryPhoneIdAsync(int phoneId) =>
            await Db.Employees
            .Where(e => e.Phones.Any(p => p.Id == phoneId))
            .FirstOrDefaultAsync();
    }
}