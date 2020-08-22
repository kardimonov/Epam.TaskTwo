namespace ITAcademy.TaskTwo.Data.Models
{
    public class Settings
    {
        public int NameMinLength { get; set; }
        public int NameMaxLength { get; set; }
        public int EmailMaxLength { get; set; }
        public string PhonePattern { get; set; }
        public int SubjectNameMinLength { get; set; }
        public int SubjectNameMaxLength { get; set; }
        public int PositionNameMinLength { get; set; }
        public int PositionNameMaxLength { get; set; }
        public int EmailMessageContent { get; set; }
        public int SmsMessageContent { get; set; }

        public int UserNameMinLength { get; set; }
        public int UserNameMaxLength { get; set; }
        public int PasswordMinLength { get; set; }
        public int PasswordMaxLength { get; set; }
    }
}