using System;
using ITAcademy.TaskTwo.Data.Interfaces;

namespace ITAcademy.TaskTwo.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext db;
        private IEmployeeRepository employeeRepository;
        private ISubjectRepository subjectRepository;
        private IPositionRepository positionRepository;
        private IPhoneRepository phoneRepository;
        private IMessageRepository messageRepository;
        private bool disposed = false;

        public UnitOfWork(IApplicationContext context)
        {
            db = context;
        }

        public IEmployeeRepository EmployeeRepo => employeeRepository ??= new EmployeeRepository(db);

        public ISubjectRepository SubjectRepo => subjectRepository ??= new SubjectRepository(db);

        public IPositionRepository PositionRepo => positionRepository ??= new PositionRepository(db);

        public IPhoneRepository PhoneRepo => phoneRepository ??= new PhoneRepository(db);

        public IMessageRepository MessageRepo => messageRepository ??= new MessageRepository(db);

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}