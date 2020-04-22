namespace MDManagement.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static MDManagement.Common.DataValidations.Adress;

    public class Address
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(AddressMaxLength)]
        public string AddressText { get; set; }

        public int TownId { get; set; }

        public Town Town { get; set; }

        public string EmployeeId { get; set; }

        public Employee Employee { get; set; }

    }
}
