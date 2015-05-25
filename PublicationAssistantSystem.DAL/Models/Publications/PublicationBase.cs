using System;
using System.Collections.Generic;
using PublicationAssistantSystem.DAL.Models.Misc;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.Models.Publications
{
    public abstract class PublicationBase
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}