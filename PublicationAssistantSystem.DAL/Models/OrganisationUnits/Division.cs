using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Division
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("Institute")]
        public int InstituteId { get; set; }

        public Institute Institute { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}