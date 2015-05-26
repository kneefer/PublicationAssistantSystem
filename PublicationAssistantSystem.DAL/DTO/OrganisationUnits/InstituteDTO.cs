using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.DAL.DTO.OrganisationUnits
{
    public class InstituteDTO
    {
        public InstituteDTO() {}
        public InstituteDTO(Institute institute)
        {
            Id = institute.Id;
            Name = institute.Name;
            FacultyId = institute.Faculty.Id;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
    }
}
