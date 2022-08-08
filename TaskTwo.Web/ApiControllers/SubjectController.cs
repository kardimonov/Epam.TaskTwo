using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.SubjectDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.ApiControllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService service;

        public SubjectController(ISubjectService serv)
        {
            service = serv;
        }

        // GET: api/Subject
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubjectWithEmployeesDto>>> GetAll()
        {
            var subjects = await service.GetAllSubjectsWithEmployeesAsync();
            if (subjects == null)
            {
                return NotFound();
            }
            return Ok(subjects);
        }

        // GET: api/Subject/5
        [HttpGet("{name}")]
        public async Task<ActionResult<SubjectWithEmployeesDto>> GetByName(string name)
        {
            if (!string.IsNullOrWhiteSpace(name) && service.ExistsName(name))
            {
                var subject = await service.GetSubjectWithEmployeesAsync(name);
                if (subject != null)
                {
                    return Ok(subject);
                }
            }
            return NotFound();
        }
    }
}