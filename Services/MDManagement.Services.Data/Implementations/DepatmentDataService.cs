namespace MDManagement.Services.Data.Implementations
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.Department;
    using MDManagement.Web.Data;
    using System.Linq;

    public class DepatmentDataService : IDepatmentDataService
    {
        private readonly MDManagementDbContext data;

        public DepatmentDataService(MDManagementDbContext data)
        {
            this.data = data;
        }

        public void Create(string name)
        {
            var department = new Department()
            {
                Name = name
            };

            data.Departments.Add(department);

            data.SaveChanges();
        }

        public bool Exists(string departmentName)
        {
            return data.Departments.Any(d => d.Name == departmentName);
        }

        public DepartmentServiceModel FindByName(string name)
        {
            return data.Departments
                .Where(d => d.Name == name)
                .Select(d => new DepartmentServiceModel
                {
                    DepartmentId = d.Id,
                    DepartmentName = d.Name,
                })
                .FirstOrDefault();
        }
    }
}
