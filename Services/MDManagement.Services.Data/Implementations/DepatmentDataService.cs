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

        /// <summary>
        /// Creates an Department
        /// </summary>
        /// <param name="name">Department name</param>
        public void Create(string name)
        {
            var department = new Department()
            {
                Name = name
            };

            data.Departments.Add(department);

            data.SaveChanges();
        }

        /// <summary>
        /// Checks by department name if company really exists 
        /// </summary>
        /// <param name="departmentName">Department name</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(string departmentName)
        {
            return data.Departments.Any(d => d.Name.ToLower() == departmentName.ToLower());
        }
        /// <summary>
        /// Checks by department name if company really exists 
        /// </summary>
        /// <param name="id">Department id</param>
        /// <returns>Returns true if exists</returns>
        public bool Exists(int? id)
        {
            return data.Departments.Any(d => d.Id == id);

        }

        /// <summary>
        /// Finds a Department by id
        /// </summary>
        /// <param name="id">Department id</param>
        /// <returns>DepartmentServiceModel which contains the needed info</returns>
        public DepartmentServiceModel FindById(int? id)
        {
            if (!Exists(id))
            {
                return new DepartmentServiceModel
                {
                    DepartmentId = 0,
                    DepartmentName = " "
                };
            }

            return data.Departments
                .Where(d => d.Id == id)
                .Select(d => new DepartmentServiceModel 
                {
                    DepartmentId = d.Id,
                    DepartmentName = d.Name
                })
                .FirstOrDefault();
        }

        /// <summary>
        /// Finds a Department by name
        /// </summary>
        /// <param name="name">Department name</param>
        /// <returns>DepartmentServiceModel which contains the needed info</returns>
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
