using System.Collections.Generic;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Hubs;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;
using Microsoft.AspNetCore.SignalR;

namespace ITAcademy.TaskTwo.Logic.Decorators
{
    public class PositionDecorator : BaseDecorator<Position>, IPositionDecorator
    {
        private readonly IApplicationContext db;
        private readonly IHubContext<SignalHub> hub;
        private readonly IMapper mapper;

        public PositionDecorator(
            IApplicationContext context,
            IPositionRepository repository,
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
                var positions = mapper.Map<IEnumerable<PositionDto>>(GetAll());
                await hub.Clients.All.SendAsync("UpdatePositionList", positions);
            };
        }
    }
}