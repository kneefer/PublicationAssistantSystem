using System;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Misc
{
    public class JournalEditionDTO
    {
        public JournalEditionDTO() { }

        public JournalEditionDTO(JournalEdition journal)
        {
            Id = journal.Id;
            PublishDate = journal.PublishDate;
            VolumeNumber = journal.VolumeNumber;
            JournalId = journal.Journal.Id;
        }

        public int Id { get; set; }
        public DateTime PublishDate { get; set; }
        public int VolumeNumber { get; set; }
        public int JournalId { get; set; }
    }
}
