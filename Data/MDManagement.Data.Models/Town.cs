namespace MDManagement.Data.Models
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using static MDManagement.Common.DataValidations.Town;

    public class Town
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(PostCodeMaxLength)]
        public int PostCode { get; set; }

        public ICollection<Address> Adresses { get; set; } = new HashSet<Address>();
    }
}
