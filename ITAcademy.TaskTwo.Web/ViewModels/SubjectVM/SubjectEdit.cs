using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ViewModels.SubjectVM
{
    public class SubjectEdit
    {
        public int Id { get; set; }

        [Remote(action: "VerifyName", controller: "Subject", ErrorMessage = "Такой предмет уже существует в базе")]
        [Display(Name = "Название учебного предмета")]
        public string Name { get; set; }
    }
}