using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Logic.Models.PositionDTO;
using TaskTwo.Web.ViewModels.PositionVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.Controllers
{
    [Authorize]
    public class PositionController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPositionDecorator decorator; 
        private readonly IPositionService service;

        public PositionController(IMapper map, IPositionDecorator decor, IPositionService serv)
        {
            mapper = map;
            decorator = decor;
            service = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var positions = await decorator.GetAllAsync();
            var model = mapper.Map<IEnumerable<PositionIndex>>(positions);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id != null)
            {
                var position = await service.GetDetailsAsync((int)id);
                if (position != null)
                {
                    return View(mapper.Map<PositionDetails>(position));
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
        public async Task<IActionResult> Create(PositionCreate model)
        {
            if (ModelState.IsValid)
            {
                var position = mapper.Map<Position>(model);
                await decorator.CreateAsync(position);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                var position = await decorator.GetAsync((int)id);
                if (position != null)
                {
                    return View(mapper.Map<PositionEdit>(position));
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(PositionEdit model)
        {
            if (ModelState.IsValid)
            {
                decorator.Update(mapper.Map<Position>(model));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditAppointedEmployees(int? id)
        {
            if (id != null)
            {
                var position = await service.GetDetailsAsync((int)id);
                if (position != null)
                {
                    var model = await service.GetEmployeesOfPositionAsync(position);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditAppointedEmployees(PositionWithEmployees model)
        {
            if (ModelState.IsValid)
            {
                await service.UpdateEmployeesOfPositionAsync(model);
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
                var position = await decorator.GetAsync((int)id);
                if (position != null)
                {
                    return View(mapper.Map<PositionDelete>(position));
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
            var positions = string.IsNullOrWhiteSpace(searchString) ?
                await decorator.GetAllAsync() :
                await decorator.SearchAsync(searchString);
            var model = mapper.Map<IEnumerable<PositionIndex>>(positions);

            return Json(new { searchString, model });
        }

        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyName(string name)
        {
            return Json(!service.ExistsName(name));
        }
    }
}