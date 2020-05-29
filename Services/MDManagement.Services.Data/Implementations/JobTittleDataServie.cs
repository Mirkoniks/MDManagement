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

        public JobTittleDataServie(MDManagementDbContext data
                                    )
        {
            this.data = data;
        }

        /// <summary>
        /// Find a job tile by id
        /// </summary>
        /// <param name="id">Job title id</param>
        /// <returns>JobTitleServiceModel which is a DTO which contains the needed info for this operations</returns>
        public JobTitleServiceModel FindById(int? id)
        {
            var jtsm = new JobTitleServiceModel();

            if (!Exists(id))
            {
                jtsm.Id = 0;
                jtsm.Name = " ";
                jtsm.Description = "Nothing For Now";

                return jtsm;
            }

            var jobTile = data.JobTitles.Where(jt => jt.Id == id).FirstOrDefault();

            jtsm.Id = jobTile.Id;
            jtsm.Name = jobTile.Name;
            jtsm.Description = "Nothing For Now";

            return jtsm;
        }


        /// <summary>
        /// Finds a job title by name
        /// </summary>
        /// <param name="name">Job title name</param>
        /// <returns>JobTitleServiceModel which is a DTO which contains the needed info for this operations</returns>
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

        /// <summary>
        /// Creates a Job Tile  
        /// </summary>
        /// <param name="name">Job title name</param>
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

        /// <summary>
        /// Checks by name if job title exists
        /// </summary>
        /// <param name="jobTitle">Job title name</param>
        /// <returns></returns>
        public bool Exists(string jobTitle)
        {
            return data.JobTitles.Any(j => j.Name.ToLower() == jobTitle.ToLower());
        }

        /// <summary>
        /// Checks by id if job title exists
        /// </summary>
        /// <param name="jobId">Job title id</param>
        /// <returns></returns>
        public bool Exists(int? jobId)
        {
            return data.JobTitles.Any(j => j.Id == jobId);
        }
    }
}
