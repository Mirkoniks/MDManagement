using MDManagement.Services.Data;

namespace MDManagement.Services.Implementations
{
    public class JobTitleService : IJobTitleService
    {
        private readonly IJobTitleDataService jobTitleDataService;

        public JobTitleService(IJobTitleDataService jobTitleDataService)
        {
            this.jobTitleDataService = jobTitleDataService;
        }

        //public void AddJobTitle(string name)
        //{
        //    if (!jobTitleDataService.Exists(name))
        //    {
        //        jobTitleDataService.CreateJobTitile(name);
        //    }
        //}
    }
}
