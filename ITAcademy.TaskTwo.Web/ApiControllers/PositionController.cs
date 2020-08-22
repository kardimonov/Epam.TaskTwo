using System.Collections.Generic;
using System.Threading.Tasks;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Logic.Models.PositionDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ApiControllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService service;

        public PositionController(IPositionService serv)
        {
            service = serv;
        }

        // GET: api/Position
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PositionWithEmployeesDto>>> GetAll()
        {
            var positions = await service.GetAllPositionsWithEmployeesAsync();
            if (positions == null)
            {
                return NotFound();
            }
            return Ok(positions);
        }

        // GET: api/Position/5
        [HttpGet("{name}")]
        public async Task<ActionResult<PositionWithEmployeesDto>> GetByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && service.ExistsName(name))
            {
                var position = await service.GetPositionWithEmployeesAsync(name);
                if (position != null)
                {
                    return Ok(position);
                }
            }
            return NotFound();
        }
    }
}