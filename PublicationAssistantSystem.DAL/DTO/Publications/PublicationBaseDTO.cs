using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.DTO.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    public abstract class PublicationBaseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Discriminator { get; set; }

        public IEnumerable<EmployeeDTO> Employees { get; set; }
    }
}