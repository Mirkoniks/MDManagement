﻿namespace MDManagement.Web.ViewModels.Management
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    public class EditUserViewModel
    {
        public string EmployeeId { get; set; }

        public DateTime HireDate { get; set; }

        public decimal? Salary { get; set; }

        public string JobTitle { get; set; }

        [Required]       
        public string Department { get; set; }

        public string ManagerNickname { get; set; }
    }
}
