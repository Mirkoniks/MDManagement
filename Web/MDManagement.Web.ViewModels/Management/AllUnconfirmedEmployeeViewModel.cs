namespace MDManagement.Web.ViewModels.Management
{
    using System.Collections.Generic;

    public class AllUnconfirmedEmployeeViewModel
    {
        public IEnumerable<UnconfirmedEmployeeViewModel> AllUnconfirmedEmployees { get; set; }
    }
}
