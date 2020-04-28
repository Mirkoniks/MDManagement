namespace MDManagement.Services
{
    using MDManagement.Services.Models.Company;

    public interface IComapnyService
    {
        public void Register(CreateCompanyServiceModel model);

        public CompanyServiceModel FindByName(string name);

        public bool Exists(string companyName);
    }
}
