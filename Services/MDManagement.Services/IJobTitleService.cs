namespace MDManagement.Services
{
    using System.Security.Claims;

    public interface IJobTitleService
    {
        public void AddJobTitle(string name, ClaimsPrincipal user);
    }
}
