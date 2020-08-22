using System;
using ITAcademy.TaskTwo.Data.Configurations;
using ITAcademy.TaskTwo.Data.Interfaces;
using ITAcademy.TaskTwo.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ITAcademy.TaskTwo.Data.Context
{
    public class ApplicationContext : IdentityDbContext<User>, IApplicationContext //DbContext,
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        { }

        public event EventHandler OnChangesSaved;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Message> Messages { get; set; }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            if (result > 0)
            {
                OnChangesSaved?.Invoke(this, EventArgs.Empty);
            }
            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfiguration).Assembly);
        }
    }
}