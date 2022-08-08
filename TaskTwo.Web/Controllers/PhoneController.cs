using System.Threading.Tasks;
using AutoMapper;
using TaskTwo.Data.Models;
using TaskTwo.Logic.Interfaces;
using TaskTwo.Web.ViewModels.PhoneVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTwo.Web.Controllers
{
    [Authorize]
    public class PhoneController : Controller
    {
        private readonly IMapper mapper;
        private readonly IPhoneDecorator decorator;
        private readonly IEmployeeService service;

        public PhoneController(IMapper map, IPhoneDecorator decor, IEmployeeService serv)
        {
            mapper = map;
            decorator = decor;
            service = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            var employee = await service.GetAsync((int)id);
            var model = mapper.Map<PhoneIndex>(employee);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id, string method)
        {
            var employee = await service.GetAsync((int)id); 
            var model = mapper.Map<PhoneCreate>(employee, opt => opt.Items["Method"] = method);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PhoneCreate model)
        {
            if (ModelState.IsValid)
            {                
                if (!string.IsNullOrWhiteSpace(model.Number))
                {
                    var phone = mapper.Map<Phone>(model);
                    await decorator.CreateAsync(phone);
                    await service.SetPrimaryPhoneAsync(phone);
                }
                return model.Method == "Edit" ? 
                    RedirectToAction("Edit", "Employee", new { id = model.EmployeeId }) :
                    RedirectToAction("Index", "Phone", new { id = model.EmployeeId });
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id, string method)
        {
            if (id != null)
            {
                var phone = await decorator.GetAsync((int)id);
                if (phone != null)
                {
                    var model = mapper.Map<PhoneEdit>(phone, opt => opt.Items["Method"] = method);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Edit(PhoneEdit model)
        {
            if (ModelState.IsValid)
            {
                var phone = mapper.Map<Phone>(model);
                decorator.Update(phone);

                return model.Method == "Edit" ?
                    RedirectToAction("Edit", "Employee", new { id = model.EmployeeId }) :
                    RedirectToAction("Index", "Phone", new { id = model.EmployeeId });
            }
            return View(model);
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id, string method)
        {
            if (id != null)
            {
                var phone = await decorator.GetAsync((int)id);
                if (phone != null)
                {
                    var model = mapper.Map<PhoneDelete>(phone, opt => opt.Items["Method"] = method);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id, string method)
        {
            if (id != null)
            {
                var phone = await decorator.GetAsync((int)id);                
                await service.SetPrimaryPhoneAsync(phone);
                await decorator.DeleteAsync((int)id);
                return method == "Edit" ?
                    RedirectToAction("Edit", "Employee", new { id = phone.EmployeeId }) :
                    RedirectToAction("Index", "Phone", new { id = phone.EmployeeId });
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPrimaryPhone(int? phoneId)
        {
            await service.ResetPrimaryPhoneAsync((int)phoneId);
            return Json(new { phoneId });
        }
    }
}