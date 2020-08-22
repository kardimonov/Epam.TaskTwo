using System.ComponentModel.DataAnnotations;

namespace ITAcademy.TaskTwo.Web.ViewModels.PositionVM
{
    public class PositionEdit
    {
        public int Id { get; set; }

        [Display(Name = "Название должности")]
        public string Name { get; set; }

        [Display(Name = "Максимальное количество штатных единиц")]
        public int MaxNumber { get; set; }
    }
}