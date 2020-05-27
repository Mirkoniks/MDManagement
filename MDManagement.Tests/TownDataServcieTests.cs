using MDManagement.Services.Data;
using MDManagement.Services.Data.Implementations;
using MDManagement.Services.Models.Address;
using MDManagement.Services.Models.Town;
using MDManagement.Web.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace MDManagement.Tests
{
    public class TownDataServcieTests
    {
        [SetUp]
        public static void Setup()
        {

        }

        [Test]
        public void DoesTheSerivceAddTownToTheDataBase()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
               .UseInMemoryDatabase(databaseName: "testDb")
               .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ITownDataService service = new TownDataService(dbContext);

                CreateTownServiceModel serviceModel = new CreateTownServiceModel
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                service.Create(serviceModel);

                Assert.AreEqual("Pernik", dbContext.Towns.FirstOrDefault().Name);

            }
        }

        [Test]
        public void DoesExistsReturnsFalseWhenThereIsNoTown()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
           .UseInMemoryDatabase(databaseName: "testDb")
           .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ITownDataService service = new TownDataService(dbContext);

                CreateTownServiceModel serviceModel = new CreateTownServiceModel
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                service.Create(serviceModel);

                var result = service.Exists("Pernik");

                Assert.IsTrue(result);
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
                ITownDataService service = new TownDataService(dbContext);

                CreateTownServiceModel serviceModel = new CreateTownServiceModel
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                service.Create(serviceModel);

                var result = service.FindById(1).Name;

                Assert.AreEqual("Pernik", result);
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
                ITownDataService service = new TownDataService(dbContext);

                CreateTownServiceModel serviceModel = new CreateTownServiceModel
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                service.Create(serviceModel);

                var result = service.FindByName("Pernik").Name;

                Assert.AreEqual("Pernik", result);
            }
        }
    }
}



