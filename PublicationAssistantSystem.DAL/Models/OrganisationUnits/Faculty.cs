using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Faculty
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Please enter : Id")]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        [Display(Name = "Abbreviation")]
        public string Abbreviation { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [JsonIgnore]
        [Display(Name = "Institutes")]
        public virtual ICollection<Institute> Institutes { get; set; }
    }
}