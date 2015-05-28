using System;

namespace PublicationAssistantSystem.DAL.DTO.Misc
{
    public class JournalEditionDTO
    {
        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public int VolumeNumber { get; set; }
        public int JournalId { get; set; }
    }
}