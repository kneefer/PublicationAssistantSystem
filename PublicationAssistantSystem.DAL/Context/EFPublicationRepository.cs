using System.Data.Entity;
using System.Linq;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Context
{
    public class EFPublicationRepository : IPublicationRepository
    {
        private readonly PublicationAssistantContext _context = new PublicationAssistantContext();

        public IDbSet<Employee> Employees { get { return _context.Employees; } }
        public IDbSet<Journal> Journals { get { return _context.Journals; } }
        public IDbSet<JournalEdition> JournalEditions { get { return _context.JournalEditions; } }
        public IDbSet<Division> Divisions { get { return _context.Divisions; } }
        public IDbSet<Faculty> Faculties { get { return _context.Faculties; } }
        public IDbSet<Institute> Institutes { get { return _context.Institutes; } }
        public IDbSet<PublicationBase> Publications { get { return _context.Publications; } }
    }
}