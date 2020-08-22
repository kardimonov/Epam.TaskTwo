using System;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository EmployeeRepo { get; }

        ISubjectRepository SubjectRepo { get; }

        IPositionRepository PositionRepo { get; }

        IPhoneRepository PhoneRepo { get; }

        IMessageRepository MessageRepo { get; }

        void Save();
    }
}