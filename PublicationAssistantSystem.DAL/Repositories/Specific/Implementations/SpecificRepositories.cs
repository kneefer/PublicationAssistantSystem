using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.DAL.Repositories.Generic;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.DAL.Repositories.Specific.Implementations
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class JournalRepository : GenericRepository<Journal>, IJournalRepository
    {
        public JournalRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class JournalEditionRepository : GenericRepository<JournalEdition>, IJournalEditionRepository
    {
        public JournalEditionRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class DivisionRepository : GenericRepository<Division>, IDivisionRepository
    {
        public DivisionRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class FacultyRepository : GenericRepository<Faculty>, IFacultyRepository
    {
        public FacultyRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class InstituteRepository : GenericRepository<Institute>, IInstituteRepository
    {
        public InstituteRepository(IPublicationAssistantContext context)
            : base(context) { }
    }

    public class PublicationRepository : GenericRepository<PublicationBase>, IPublicationBaseRepository
    {
        public PublicationRepository(IPublicationAssistantContext context)
            : base(context) { }
    }
}
