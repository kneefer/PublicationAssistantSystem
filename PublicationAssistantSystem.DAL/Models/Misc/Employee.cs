using System.Collections.Generic;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class Employee
    {
        public int Id { get; set; }
        public string AcademicTitle { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public Division Division { get; set; }
        public virtual ICollection<PublicationBase> Publications { get; set; }
    }
}