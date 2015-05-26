using PublicationAssistantSystem.DAL.Models.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Misc
{
    public class EmployeeDTO
    {
        public EmployeeDTO() { }

        public EmployeeDTO(Employee employee)
        {
            Id            = employee.Id;
            AcademicTitle = employee.AcademicTitle;
            FirstName     = employee.FirstName;
            LastName      = employee.LastName;
            DivisionId    = employee.Division.Id;
        }

        public int Id { get; set; }
        public string AcademicTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int DivisionId { get; set; }
    }
}
