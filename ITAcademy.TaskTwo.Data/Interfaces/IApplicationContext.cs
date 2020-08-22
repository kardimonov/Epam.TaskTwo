using ITAcademy.TaskTwo.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace ITAcademy.TaskTwo.Data.Interfaces
{
    public interface IApplicationContext
    {
        event EventHandler OnChangesSaved;
        
        DbSet<Employee> Employees { get; set; }
        DbSet<Subject> Subjects { get; set; }
        DbSet<Position> Positions { get; set; }
        DbSet<Phone> Phones { get; set; }
        DbSet<Message> Messages { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;        
        EntityEntry<TEntity> Attach<TEntity>(TEntity item) where TEntity : class;
        EntityEntry<TEntity> Entry<TEntity>(TEntity item) where TEntity : class;         
        int SaveChanges();
        void Dispose();
    }
}