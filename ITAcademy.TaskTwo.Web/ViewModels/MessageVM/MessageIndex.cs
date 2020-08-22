using ITAcademy.TaskTwo.Data.Enums;
using System;

namespace ITAcademy.TaskTwo.Web.ViewModels.MessageVM
{
    public class MessageIndex
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Content { get; set; }

        public MessageType Type { get; set; }

        public DateTime TimeCreated { get; set; }

        public MessageStatus DispatchResult { get; set; }
    }
}