using System.Collections.Generic;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Hubs;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.SubjectDTO;
using Microsoft.AspNetCore.SignalR;

namespace ITAcademy.TaskTwo.Logic.Decorators
{
    public class SubjectDecorator : BaseDecorator<Subject>, ISubjectDecorator
    {
        private readonly IApplicationContext db;
        private readonly IHubContext<SignalHub> hub;
        private readonly IMapper mapper;

        public SubjectDecorator(
            IApplicationContext context,
            ISubjectRepository repository,
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
                var employees = mapper.Map<IEnumerable<SubjectDto>>(GetAll());
                await hub.Clients.All.SendAsync("UpdateSubjectList", employees);
            };
        }
    }
}