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

        /// <summary>
        /// Finds a Company by id
        /// </summary>
        /// <param name="id">Company id</param>
        /// <returns>Company service model which contains the needed info</returns>
        public CompanyServiceModel FindById(int? id)
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
        /// <summary>
        /// Finds a Company by company code
        /// </summary>
        /// <param name="companyCode">Company code</param>
        /// <returns>Company service model which contains the needed info</returns>
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

        /// <summary>
        /// Find a company by id
        /// </summary>
        /// <param name="name">Company name</param>
        /// <returns>Company service model which contains the needed info</returns>
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
        /// <summary>
        /// Creates a company
        /// </summary>
        /// <param name="model">CreateCompanyServiceModel is a DTO with enough info for creating a company</param>
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
        /// <summary>
        /// Checks by company name if a company really exists
        /// </summary>
        /// <param name="companyName">Company name</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(string companyName)
        {
            return data.Companies.Any(c => c.Name.ToLower() == companyName.ToLower());
        }

        /// <summary>
        /// Checks by company code if a company really exists
        /// </summary>
        /// <param name="companyCode">Company code</param>
        /// <returns>Returns true if exists</returns>
        public bool IsValidCompany(string companyCode)
        {
            return data.Companies.Any(c => c.CompanyCode == companyCode);
        }

        /// <summary>
        /// Checks by comapny id if there is employees in this company
        /// </summary>
        /// <param name="companyId">Company id</param>
        /// <returns>Returns true if exists</returns>
        public bool HasEmployees(int? companyId)
        {
            return data.Companies
                .Where(c => c.Id == companyId)
                .Any(c => c.Employees.Count > 0);
        }
    }
}
