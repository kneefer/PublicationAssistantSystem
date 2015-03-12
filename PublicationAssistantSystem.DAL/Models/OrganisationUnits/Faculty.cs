using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    [MetadataType(typeof(FacultyMetadata))]
    public class Faculty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Institute> Institutes { get; set; }
    }

    public partial class FacultyMetadata
    {

        [Display(Name = "Id")]
        [Required(ErrorMessage = "Please enter : Id")]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Institutes")]
        public Institute Institutes { get; set; }
    }
}