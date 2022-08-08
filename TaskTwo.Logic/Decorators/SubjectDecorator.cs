using System.Collections.Generic;
using AutoMapper;
using TaskTwo.Data.Interfaces;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Hubs;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.SubjectDTO;
using Microsoft.AspNetCore.SignalR;

namespace TaskTwo.Logic.Decorators
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