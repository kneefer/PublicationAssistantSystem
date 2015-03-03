using System.Collections.Generic;
using PublicationAssistantSystem.Model.Misc;

namespace PublicationAssistantSystem.Model.OrganisationUnits
{
    public class Division
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Institute Institute { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}