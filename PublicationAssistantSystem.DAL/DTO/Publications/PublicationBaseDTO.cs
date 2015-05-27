using System;
using System.ComponentModel.DataAnnotations;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    public abstract class PublicationBaseDTO
    {
        public PublicationBaseDTO() { }

        public PublicationBaseDTO(PublicationBase publicationBase)
        {
            Id = publicationBase.Id;
            Title = publicationBase.Title;
            PublicationDate = publicationBase.PublicationDate;
        }

        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Discriminator { get; set; }
    }
}