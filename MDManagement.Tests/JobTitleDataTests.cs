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
    public class JobTitleDataTests
    {
        [Test]
        public void DoesCreateJobTitleWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                IJobTitleDataService service = new JobTittleDataServie(dbContext);

                service.CreateJobTitile("CEO");

                Assert.AreEqual("CEO", dbContext.JobTitles.FirstOrDefault().Name);
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
                IJobTitleDataService service = new JobTittleDataServie(dbContext);

                service.CreateJobTitile("CEO");

                var result = service.Exists("CEO");

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
                IJobTitleDataService service = new JobTittleDataServie(dbContext);

                service.CreateJobTitile("CEO");

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
                IJobTitleDataService service = new JobTittleDataServie(dbContext);

                service.CreateJobTitile("CEO");

                var result = service.FindByName("CEO").Name;

                Assert.AreEqual("CEO", result);
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
                IJobTitleDataService service = new JobTittleDataServie(dbContext);

                service.CreateJobTitile("CEO");

                var result = service.FindById(1).Name;

                Assert.AreEqual("CEO", result);
            }
        }
    }
}
