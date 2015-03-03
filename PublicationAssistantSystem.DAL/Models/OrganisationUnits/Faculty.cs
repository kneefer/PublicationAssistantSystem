using System.Collections.Generic;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}