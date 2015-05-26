using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace PublicationAssistantSystem.DAL.Models.OrganisationUnits
{
    public class Faculty
    {
        [Required(ErrorMessage = "Please enter : Id")]
        public int Id { get; set; }

        [MaxLength(10)]
        [Required]
        public string Abbreviation { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Institute> Institutes { get; set; }
    }
}