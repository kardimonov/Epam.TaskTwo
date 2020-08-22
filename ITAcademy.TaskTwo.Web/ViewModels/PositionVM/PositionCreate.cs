using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ITAcademy.TaskTwo.Web.ViewModels.PositionVM
{
    public class PositionCreate
    {
        [Remote(action: "VerifyName", controller: "Position", ErrorMessage = "Такая должность уже существует в базе")]
        [Display(Name = "Название должности")]
        public string Name { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Введено некорректное значение")]
        [Display(Name = "Максимальное количество штатных единиц")]
        public int MaxNumber { get; set; }
    }
}
