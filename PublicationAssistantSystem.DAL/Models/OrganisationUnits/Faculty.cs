using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Faculty
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Please enter : Id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [JsonIgnore]
        [Display(Name = "Institutes")]
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}