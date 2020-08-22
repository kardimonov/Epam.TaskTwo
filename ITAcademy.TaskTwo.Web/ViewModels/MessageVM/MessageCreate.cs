using ITAcademy.TaskTwo.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ITAcademy.TaskTwo.Web.ViewModels.MessageVM
{
    public class MessageCreate
    {
        public int AddresseeId { get; set; }

        [Display(Name = "Получатель:")]
        public string FullName { get; set; }

        [Display(Name = "Текст сообщения")]
        public string Content { get; set; }
        
        public MessageType Type { get; set; }

        public int MaxLength { get; set; }        
    }
}