using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.SubjectDTO;
using TaskTwo.Web.ViewModels.SubjectVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.Controllers
{
    [Authorize]
    public class SubjectController : Controller
    {        
        private readonly IMapper mapper;
        private readonly ISubjectDecorator decorator;
        private readonly ISubjectService service;

        public SubjectController(IMapper map, ISubjectDecorator decor, ISubjectService serv)
        {
            mapper = map;
            decorator = decor;
            service = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var subjects = await decorator.GetAllAsync();
            var model = mapper.Map<IEnumerable<SubjectIndex>>(subjects);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var subject = await service.GetDetailsAsync((int)id);
                if (subject != null)
                {
                    return View(mapper.Map<SubjectDetails>(subject));
                }
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectCreate model)
        {
            if (ModelState.IsValid)
            {
                var subject = mapper.Map<Subject>(model);
                await decorator.CreateAsync(subject);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var subject = await decorator.GetAsync((int)id);
                if (subject != null)
                {
                    return View(mapper.Map<SubjectEdit>(subject));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(SubjectEdit model)
        {
            if (ModelState.IsValid)
            {
                var subject = mapper.Map<Subject>(model);
                decorator.Update(subject);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAssignedEmployees(int? id)
        {
            if (id != null)
            {
                var subject = await service.GetDetailsAsync((int)id);
                if (subject != null)
                {
                    var model = await service.GetEmployeesOfSubjectAsync(subject);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditAssignedEmployees(SubjectWithEmployees model)
        {
            if (ModelState.IsValid)
            {
                service.UpdateEmployeesOfSubject(model);
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
                var subject = await decorator.GetAsync((int)id);
                if (subject != null)
                {
                    return View(mapper.Map<SubjectDelete>(subject));
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
            var subjects = string.IsNullOrWhiteSpace(searchString) ?
                await decorator.GetAllAsync() : 
                await decorator.SearchAsync(searchString);
            var model = mapper.Map<IEnumerable<SubjectIndex>>(subjects);

            return Json(new { searchString, model });
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyName(string name)
        {
            return Json(!service.ExistsName(name));
        }
    }
}