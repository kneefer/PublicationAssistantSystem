using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Institute
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Faculty Faculty { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
    }
}