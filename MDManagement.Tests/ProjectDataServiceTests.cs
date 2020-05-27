using MDManagement.Data.Models;
using MDManagement.Services.Data;
using MDManagement.Services.Data.Implementations;
using MDManagement.Services.Models.Project;
using MDManagement.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDManagement.Tests
{
    public class ProjectDataServiceTests
    {
        
        public void DoesCreateWorks()
        {
       
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                var userManager = new Mock<UserManager<Employee>>().Object;
                IJobTitleDataService jobTitleService = new JobTittleDataServie(dbContext);
                ICompanyDataService companyService = new CompanyDataSerive(dbContext);
                IEmployeeDataService employeeService = new EmployeeDataService(dbContext, jobTitleService, companyService, userManager);
                IProjectDataService projectService = new ProjectDataService(dbContext, employeeService);

                var modelService = new ProjectServiceModel
                {
                    Name = "AI",
                    Description = "nope",
                };

                Assert.AreEqual("AI", dbContext.Projects.FirstOrDefault().Name);
            }
        }
    }
    
}
