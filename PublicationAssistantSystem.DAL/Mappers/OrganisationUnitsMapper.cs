using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;

namespace PublicationAssistantSystem.DAL.Mappers
{
    public static class OrganisationUnitsMapper
    {
        public static FacultyDTO ToDTO(this Faculty faculty)
        {
            return  new FacultyDTO(faculty);
        }

        public static InstituteDTO ToDTO(this Institute institute)
        {
            return new InstituteDTO(institute);
        }

        public static DivisionDTO ToDTO(this Division division)
        {
            return new DivisionDTO(division);
        }
    }
}