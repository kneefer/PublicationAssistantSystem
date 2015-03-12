using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Generic;

namespace PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {

    }

    public interface IJournalRepository : IGenericRepository<Journal>
    {

    }

    public interface IJournalEditionRepository : IGenericRepository<JournalEdition>
    {

    }

    public interface IDivisionRepository : IGenericRepository<Division>
    {

    }

    public interface IFacultyRepository : IGenericRepository<Faculty>
    {

    }

    public interface IInstituteRepository : IGenericRepository<Institute>
    {

    }

    public interface IPublicationBaseRepository : IGenericRepository<PublicationBase>
    {

    }
}
