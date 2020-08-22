using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Web.ViewModels.EmployeeVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEmployeeDecorator decorator; 
        private readonly IEmployeeService service;

        public EmployeeController(IMapper map, IEmployeeDecorator decor, IEmployeeService serv)
        {
            mapper = map;
            decorator = decor;
            service = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await service.GetEmployeesWithPhonesAsync();
            var model = mapper.Map<IEnumerable<EmployeeIndex>>(employees);
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeCreate model)
        {
            if (ModelState.IsValid)
            {
                var employee = mapper.Map<Employee>(model);
                await decorator.CreateAsync(employee);
                return RedirectToAction("Index", "Phone", new { id = employee.Id });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var employee = await decorator.GetAsync((int)id);
                if (employee != null)
                {
                    var model = mapper.Map<EmployeeEdit>(employee);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeEdit model)
        {
            if (ModelState.IsValid)
            {
                var employee = mapper.Map<Employee>(model);
                decorator.Update(employee);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                var employee = await decorator.GetAsync((int)id);
                if (employee != null)
                {
                    var model = mapper.Map<EmployeeDelete>(employee);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                await decorator.DeleteAsync((int)id);
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Search(string searchString)
        {
            var employees = string.IsNullOrWhiteSpace(searchString) ?
                await service.GetEmployeesWithPhonesAsync() : 
                await decorator.SearchAsync(searchString);
            var model = mapper.Map<IEnumerable<EmployeeIndex>>(employees);

            return Json(new { searchString, model });
        }        
    }
}