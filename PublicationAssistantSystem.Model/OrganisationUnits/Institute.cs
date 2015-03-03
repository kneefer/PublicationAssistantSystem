using System.Collections.Generic;

namespace PublicationAssistantSystem.Model.OrganisationUnits
{
    public class Institute
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Faculty Faculty { get; set; }
        public virtual ICollection<Division> Divisions { get; set; }
    }
}