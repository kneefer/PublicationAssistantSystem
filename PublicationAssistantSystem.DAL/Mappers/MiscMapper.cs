using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.Mappers
{
    public static class MiscMapper
    {
        public static EmployeeDTO ToDTO(this Employee employee)
        {
            return new EmployeeDTO(employee);
        }

        public static JournalDTO ToDTO(this Journal journal)
        {
            return new JournalDTO(journal);
        }

        public static JournalEditionDTO ToDTO(this JournalEdition journalEdition)
        {
            return new JournalEditionDTO(journalEdition);
        }
    }
}