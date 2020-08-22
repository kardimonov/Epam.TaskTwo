using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ViewModels.SubjectVM
{
    public class SubjectCreate
    {
        [Remote(action: "VerifyName", controller: "Subject", ErrorMessage = "Такой предмет уже существует в базе")]
        [Display(Name = "Название предмета")]
        public string Name { get; set; }
    }
}