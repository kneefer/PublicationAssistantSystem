using System.Data.Entity;
using System.Linq;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.DAL.Context
{
    public interface IPublicationRepository
    {
        IDbSet<Employee> Employees { get; }
        IDbSet<Journal> Journals { get; }
        IDbSet<JournalEdition> JournalEditions { get; }
        IDbSet<Division> Divisions { get; }
        IDbSet<Faculty> Faculties { get; }
        IDbSet<Institute> Institutes { get; }
        IDbSet<PublicationBase> Publications { get; }
    }
}