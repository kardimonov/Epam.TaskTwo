using System.Collections.Generic;
using System.Threading.Tasks;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.EmployeeDTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.ApiControllers
{    
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService service;

        public EmployeeController(IEmployeeService serv)
        {
            service = serv;
        }

        // GET: api/Employee
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeWithAllLists>>> GetAll()
        {
            var employees = await service.GetAllItemsWithAllListsAsync();
            if (employees == null)
            {
                return NotFound();
            }
            return Ok(employees);
        }

        // GET: api/Employee/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeWithAllLists>> GetById(int? id)
        {
            if (id != null)
            {
                var employee = await service.GetItemWithAllListsAsync((int)id);
                if (employee != null)
                {
                    return new ObjectResult(employee);
                }
            }
            return NotFound();
        }
    }
}