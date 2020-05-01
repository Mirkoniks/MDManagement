namespace MDManagement.Services.Data.Implementations
{
    using MDManagement.Data.Models;
    using MDManagement.Services.Models.JobTitle;
    using MDManagement.Web.Data;
    using Microsoft.EntityFrameworkCore.Internal;
    using System.Linq;

    public class JobTittleDataServie : IJobTitleDataService
    {
        private readonly MDManagementDbContext data;

        public JobTittleDataServie(MDManagementDbContext data)
        {
            this.data = data;
        }

        public JobTitleServiceModel FindById(int id)
        {
            var jobTile = data.JobTitles.Where(jt => jt.Id == id).FirstOrDefault();

            var jtsm = new JobTitleServiceModel()
            {
                Id = jobTile.Id,
                Name = jobTile.Name,
                Description = jobTile.Description
            };

            return jtsm;
        }

        public JobTitleServiceModel FindByName(string name)
        {
            var jobTile = data.JobTitles.Where(jt => jt.Name.ToLower() == name.ToLower()).FirstOrDefault();

            var jtsm = new JobTitleServiceModel()
            {
                Id = jobTile.Id,
                Name = jobTile.Name,
                Description = jobTile.Description
            };

            return jtsm;
        }

        public void CreateJobTitile(string name)
        {
            JobTitle jobTitleToAdd = new JobTitle()
            {
                Name = name,
                Description = "nothing for now",
            };

            data.JobTitles.Add(jobTitleToAdd);
            data.SaveChanges();
        }

        public bool Exists(string jobTitle)
        {
            return data.JobTitles.Any(j => j.Name.ToLower() == jobTitle.ToLower());
        }

    }
}
