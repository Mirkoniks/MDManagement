using System;
using System.Collections.Generic;
using System.Text;
using MDManagement.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MDManagement.Web.Data
{
    public class MDManagementDbContext : IdentityDbContext<Employee>
    {
        public DbSet<Address> Addresses { get; set; }

        public DbSet<Company> Companies { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<EmployeeProject> EmployeeProjects { get; set; }

        public DbSet<JobTitle> JobTitles { get; set; }

        public DbSet<Project> Projects { get; set; }

        public DbSet<Town> Towns { get; set; }

        public MDManagementDbContext(DbContextOptions<MDManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Town>()
                 .HasMany(t => t.Adresses)
                 .WithOne(a => a.Town)
                 .HasForeignKey(a => a.TownId)
                 .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .HasForeignKey(e => e.CompanyId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<JobTitle>()
                .HasMany(jt => jt.Employees)
                .WithOne(e => e.JobTitle)
                .HasForeignKey(e => e.JobTitleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Department>()
                .HasMany(d => d.Employees)
                .WithOne(e => e.Department)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EmployeeProject>().HasKey(ep => new { ep.EmployeeId, ep.ProjectId });

            builder.Entity<EmployeeProject>()
                .HasOne<Employee>(ep => ep.Employee)
                .WithMany(e => e.EmployeeProjects)
                .HasForeignKey(ep => ep.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<EmployeeProject>()
                .HasOne<Project>(ep => ep.Project)
                .WithMany(p => p.EmployeeProjects)
                .HasForeignKey(ep => ep.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithOne(a => a.Employee)
                .HasForeignKey<Address>(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Employee>()
                .HasMany(m => m.Employees)
                .WithOne(e => e.Manager)
                .HasForeignKey(e => e.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(builder);
        }
    }
}
