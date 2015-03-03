using System.Collections.Generic;
using PublicationAssistantSystem.Model.OrganisationUnits;
using PublicationAssistantSystem.Model.Publications;

namespace PublicationAssistantSystem.Model.Misc
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