using System.Collections.Generic;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Hubs;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.EmployeeDTO;
using Microsoft.AspNetCore.SignalR;

namespace ITAcademy.TaskTwo.Logic.Decorators
{
    public class EmployeeDecorator : BaseDecorator<Employee>, IEmployeeDecorator
    {
        private readonly IApplicationContext db;
        private readonly IHubContext<SignalHub> hub;
        private readonly IMapper mapper;

        public EmployeeDecorator(
            IApplicationContext context,
            IEmployeeRepository repository,
            IHubContext<SignalHub> hubContext,
            IUnitOfWork unitOfWork,
            IMapper map)
            : base(repository, unitOfWork)
        {
            db = context;
            hub = hubContext;
            mapper = map; 
        }

        protected override void NotifyWhenModified()
        {
            db.OnChangesSaved += async (sender, args) =>
            {
                var employees = mapper.Map<IEnumerable<EmployeeWithPhones>>(GetAll());
                await hub.Clients.All.SendAsync("UpdateEmployeeList", employees);
            };
        }
    }
}