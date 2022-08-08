using TaskTwo.Data.Enums;
using TaskTwo.Data.Models;
using System.Collections.Generic;

namespace TaskTwo.Web.ViewModels.PhoneVM
{
    public class PhoneIndex
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string SurName { get; set; }

        public int? PrimaryPhoneId { get; set; }

        public MessageType Communication { get; set; }

        public List<Phone> Phones { get; set; }
    }
}