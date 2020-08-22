using ITAcademy.TaskTwo.Data.Enums;
using System;

namespace ITAcademy.TaskTwo.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public int AddresseeId { get; set; }

        public string Content { get; set; }

        public MessageType Type { get; set; }

        public DateTime TimeCreated { get; set; }

        public MessageStatus DispatchResult { get; set; }

        public Employee Addressee { get; set; }
    }
}