using MDManagement.Services.Data;
using MDManagement.Services.Data.Implementations;
using MDManagement.Services.Models.Address;
using MDManagement.Services.Models.Town;
using MDManagement.Web.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDManagement.Tests
{
    public class AddressDataServiceTests
    {
        [Test]
        public void DoesCreateWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
             .UseInMemoryDatabase(databaseName: "testDb")
             .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ITownDataService townService = new TownDataService(dbContext);
                IAddressDataService service = new AddressDataService(dbContext);

                CreateTownServiceModel createTownServiceModel = new CreateTownServiceModel()
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                townService.Create(createTownServiceModel);

                CreateAddressServiceModel model = new CreateAddressServiceModel()
                {
                    AddressText = "new",
                    TownId = 1
                };

                service.Create(model);

                Assert.AreEqual("new", dbContext.Addresses.FirstOrDefault().AddressText);
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
                ITownDataService townService = new TownDataService(dbContext);
                IAddressDataService service = new AddressDataService(dbContext);

                CreateTownServiceModel createTownServiceModel = new CreateTownServiceModel()
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                townService.Create(createTownServiceModel);

                CreateAddressServiceModel model = new CreateAddressServiceModel()
                {
                    AddressText = "new",
                    TownId = 1
                };

                service.Create(model);

                var result = service.FindById(1).AddressText;

                Assert.AreEqual(result, dbContext.Addresses.FirstOrDefault().AddressText);
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
                ITownDataService townService = new TownDataService(dbContext);
                IAddressDataService service = new AddressDataService(dbContext);

                CreateTownServiceModel createTownServiceModel = new CreateTownServiceModel()
                {
                    Name = "Pernik",
                    PostCode = 2300
                };

                townService.Create(createTownServiceModel);

                CreateAddressServiceModel model = new CreateAddressServiceModel()
                {
                    AddressText = "new",
                    TownId = 1
                };

                service.Create(model);

                var result = service.FindByName("new").AddressText;

                Assert.AreEqual(result, dbContext.Addresses.FirstOrDefault().AddressText);
            }
        }
    }
}
