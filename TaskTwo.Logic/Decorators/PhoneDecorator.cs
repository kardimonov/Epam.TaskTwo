using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Hubs;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.EmployeeDTO;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace TaskTwo.Logic.Decorators
{
    public class PhoneDecorator : BaseDecorator<Phone>, IPhoneDecorator
    {
        private readonly IApplicationContext db;
        private readonly IHubContext<SignalHub> hub;
        private readonly IMapper mapper;

        public PhoneDecorator(
            IApplicationContext context,
            IPhoneRepository repository,
            IHubContext<SignalHub> hubContext,
            IUnitOfWork unit,
            IMapper map)
            : base(repository, unit)
        {
            db = context;
            hub = hubContext;
            mapper = map;
        }

        protected override void NotifyWhenModified()
        {
            db.OnChangesSaved += async (sender, args) =>
            {
                var source = db.Employees.Include(em => em.Phones).ToList();
                var employees = mapper.Map<IEnumerable<EmployeeWithPhones>>(source);
                await hub.Clients.All.SendAsync("UpdateEmployeeList", employees);
            };
        }
    }
}