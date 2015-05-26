using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.DAL.DTO.OrganisationUnits
{
    public class FacultyDTO
    {
        public FacultyDTO() { }
        public FacultyDTO(Faculty faculty)
        {
            Id           = faculty.Id;
            Abbreviation = faculty.Abbreviation;
            Name         = faculty.Name;
        }

        public int Id { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
    }
}
