using System.ComponentModel.DataAnnotations;

namespace PublicationAssistantSystem.DAL.DTO.Misc
{
    public class JournalDTO
    {
        [Required]
        public string ISSN { get; set; }
        public string eISSN { get; set; }

        [Required]
        public string Title { get; set; }

        public int Id { get; set; }
        public bool IsOnMNISZW { get; set; }
        public bool IsOnWOS { get; set; }
        public bool IsOnJCR { get; set; }
    }

    public class JournalPostDTO
    {
        [Required]
        public string ISSN { get; set; }
        public string eISSN { get; set; }

        [Required]
        public string Title { get; set; }
    }
}