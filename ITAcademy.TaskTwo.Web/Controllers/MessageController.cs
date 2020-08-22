using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using ITAcademy.TaskTwo.Logic.Interfaces;
using ITAcademy.TaskTwo.Web.ViewModels.MessageVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unit;
        private readonly IMessageService service;

        public MessageController(IMapper map, IUnitOfWork unitOfWork, IMessageService serv)
        {
            mapper = map;
            unit = unitOfWork;
            service = serv;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messages = await service.GetAllAsync();
            var model = mapper.Map<IEnumerable<MessageIndex>>(messages);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create(int? id)
        {
            if (id != null)
            {
                var employee = await unit.EmployeeRepo.GetAsync((int)id);
                if (employee != null)
                {
                    var model = mapper.Map<MessageCreate>(employee);
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MessageCreate model)
        {
            if (ModelState.IsValid)
            {
                var employee = await unit.EmployeeRepo.GetAsync(model.AddresseeId);
                var message = mapper.Map<Message>(model, opt => opt.Items["Addressee"] = employee);
                var result = await service.HandleMessageAsync(message, "Create");
                return View("Send", result);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SendAgain(int? id)
        {
            if (id != null)
            {
                var message = await service.GetWithAddresseeAsync((int)id);
                var result = await service.HandleMessageAsync(message, "SendAgain");
                return Json(new { id, result });
            }
            return NotFound();
        }
    }
}