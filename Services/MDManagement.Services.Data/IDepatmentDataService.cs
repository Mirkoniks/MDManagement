using MDManagement.Services.Models.Department;

namespace MDManagement.Services.Data
{
    public interface IDepatmentDataService
    {
        public bool Exists(string departmentName);

        public bool Exists(int? id);

        public DepartmentServiceModel FindByName(string name);

        public void Create(string name);

        public DepartmentServiceModel FindById(int? id);

    }
}
