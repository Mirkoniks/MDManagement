namespace MDManagement.Services.Data.Implementations
{
    using System;
    using System.Linq;
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Company;
    using MDManagement.Web.Data;

    public class CompanyDataSerive : ICompanyDataService
    {
        private readonly MDManagementDbContext data;

        public CompanyDataSerive(MDManagementDbContext data)
        {
            this.data = data;
        }

        public CompanyServiceModel FindById(int id)
        {
            var company = data.Companies.Where(c => c.Id == id).FirstOrDefault();

            var csm = new CompanyServiceModel()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CompanyCode = company.CompanyCode
            };

            return csm;
        }

        public CompanyServiceModel FindByCompanyCode(string companyCode)
        {
            var company = data.Companies.Where(c => c.CompanyCode == companyCode).FirstOrDefault();

            var csm = new CompanyServiceModel()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CompanyCode = company.CompanyCode
            };

            return csm;
        }

        public CompanyServiceModel FindByName(string name)
        {
            var company = data.Companies.Where(c => c.Name == name).FirstOrDefault();

            var csm = new CompanyServiceModel()
            {
                Id = company.Id,
                Name = company.Name,
                Description = company.Description,
                CompanyCode = company.CompanyCode
            };

            return csm;
        }

        public void Create(CreateCompanyServiceModel model)
        {
            Company company = new Company()
            {
                Name = model.Name,
                Description = model.Description,
                CompanyCode = $"#{DateTime.UtcNow.Day}{DateTime.UtcNow.Hour}{DateTime.UtcNow.Minute}{DateTime.UtcNow.Millisecond}"
            };

            data.Companies.Add(company);
            data.SaveChanges();
        }

        public bool Exists(string companyName)
        {
            return data.Companies.Any(c => c.Name.ToLower() == companyName.ToLower());
        }

        public bool IsValidCompany(string companyCode)
        {
            return data.Companies.Any(c => c.CompanyCode == companyCode);
        }
    }
}
