using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.DAL.DTO
{
    public class DivisionDTO
    {
        public DivisionDTO() { }
        public DivisionDTO(Division division)
        {
            Id = division.Id;
            Name = division.Name;
            InstituteId = division.Institute.Id;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int InstituteId { get; set; }
    }
}
