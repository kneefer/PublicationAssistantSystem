using System;
using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    public abstract class PublicationBaseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Discriminator { get; set; }
    }
}