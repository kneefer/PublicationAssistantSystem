using System.Collections.Generic;

namespace PublicationAssistantSystem.Model.OrganisationUnits
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}