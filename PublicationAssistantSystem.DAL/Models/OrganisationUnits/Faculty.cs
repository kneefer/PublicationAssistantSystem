using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Faculty
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Abbreviation { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}