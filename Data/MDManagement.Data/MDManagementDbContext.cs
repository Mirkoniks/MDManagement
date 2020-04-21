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

        public MDManagementDbContext(DbContextOptions<MDManagementDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {


            base.OnModelCreating(builder);
        }
    }
}
