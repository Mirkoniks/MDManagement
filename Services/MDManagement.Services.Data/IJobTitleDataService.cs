namespace MDManagement.Services.Data
{
    using MDManagement.Services.Models.JobTitle;

    public interface IJobTitleDataService
    {
        public JobTitleServiceModel FindById(int id);

        public JobTitleServiceModel FindByName(string name);

        public void CreateJobTitile(CreateJobTitleServiceModel createJobTitleServiceModel);

        public bool Exists(string jobTitle);
    }
}
