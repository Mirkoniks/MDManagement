namespace MDManagement.Web.ViewModels.Management
{
    using System;

    public class EditUserViewModel
    {
        public string UserId { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public string JobTitle { get; set; }

        public string Department { get; set; }

        public string ManagerNickname { get; set; }
    }
}
