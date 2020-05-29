using MDManagement.Services.Data;
using MDManagement.Services.Data.Implementations;
using MDManagement.Services.Models.Company;
using MDManagement.Web.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDManagement.Tests
{
    public class CompanyDataServiceTests
    {
        [Test]
        public void DoesCreateWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
          .UseInMemoryDatabase(databaseName: "testDb")
          .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var result = dbContext.Companies.FirstOrDefault().Name;

                Assert.AreEqual("MDM", result);
            }
        }

        [Test]
        public void DoesExistsWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
          .UseInMemoryDatabase(databaseName: "testDb")
          .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var result = service.Exists("MDM");

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
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var result = service.FindById(1).Name;

                Assert.AreEqual("MDM", result);
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
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var result = service.FindByName("MDM").Name;

                Assert.AreEqual("MDM", result);
            }
        }

        [Test]
        public void DoesFindByCompanyCodeWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
          .UseInMemoryDatabase(databaseName: "testDb")
          .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var companyCode = service.FindByName("MDM").CompanyCode;

                var result = service.FindByCompanyCode(companyCode).Name;

                Assert.AreEqual("MDM", result);
            }
        }

        [Test]
        public void DoesIsValidCompanyWorks()
        {
            var options = new DbContextOptionsBuilder<MDManagementDbContext>()
          .UseInMemoryDatabase(databaseName: "testDb")
          .Options;

            using (var dbContext = new MDManagementDbContext(options))
            {
                ICompanyDataService service = new CompanyDataSerive(dbContext);

                CreateCompanyServiceModel serviceModel = new CreateCompanyServiceModel
                {
                    Name = "MDM",
                    Description = "No"
                };

                service.Create(serviceModel);

                var companyCode = service.FindByName("MDM").CompanyCode;

                var result = service.IsValidCompany(companyCode);

                Assert.IsTrue(result);
            }
        }
    }
}
