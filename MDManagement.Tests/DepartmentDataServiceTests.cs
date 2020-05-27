using MDManagement.Services.Data;
using MDManagement.Services.Data.Implementations;
using MDManagement.Web.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDManagement.Tests
{
    public class DepartmentDataServiceTests
    {
        [Test]
        public void DoesCreateWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
          .UseInMemoryDatabase(databaseName: "testDb")
          .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IDepatmentDataService service = new DepatmentDataService(dbContext);

                service.Create("Design");

                Assert.AreEqual("Design", dbContext.Departments.FirstOrDefault().Name);
            }
        }

        [Test]
        public void DoesExistsByNameWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IDepatmentDataService service = new DepatmentDataService(dbContext);

                service.Create("Design");

                var result = service.Exists("Design");

                Assert.IsTrue(result);
            }
        }

        [Test]
        public void DoesExistsByIdWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IDepatmentDataService service = new DepatmentDataService(dbContext);

                service.Create("Design");

                var result = service.Exists(1);

                Assert.IsTrue(result);
            }
        }

        [Test]
        public void DoesFindByNameWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IDepatmentDataService service = new DepatmentDataService(dbContext);

                service.Create("Design");

                var result = service.FindByName("Design").DepartmentName;

                Assert.AreEqual("Design", result);
            }
        }

        [Test]
        public void DoesFindByIdWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IDepatmentDataService service = new DepatmentDataService(dbContext);

                service.Create("Design");

                var result = service.FindById(1).DepartmentName;

                Assert.AreEqual("Design", result);
            }
        }

    }    
}
