namespace PublicationAssistantSystem.DAL.Models
{
    public interface IComputableEntity
    {
        int Id { get; set; }
        bool IsComputing { get; set; }
    }
}