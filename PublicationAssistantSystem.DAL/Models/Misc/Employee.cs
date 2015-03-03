using System.Collections.Generic;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Models.Misc
{
    public class Employee
    {
        public int Id { get; set; }
        public string AcademicTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Division Division { get; set; }
        public virtual ICollection<PublicationBase> Publications { get; set; }
    }
}