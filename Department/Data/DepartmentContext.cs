using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Department.Models;

namespace Department.Data
{
    public class DepartmentContext : DbContext
    {
        public DepartmentContext (DbContextOptions<DepartmentContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department.Models.Department>()
                .HasMany(d => d.SubDepartments)
                .WithOne(d => d.ParentDepartment)
                .HasForeignKey(d => d.ParentDepartmentId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Department.Models.Department> Department { get; set; } = default!;
        public DbSet<Reminder> Reminders { get; set; } = default!;
    }
}
