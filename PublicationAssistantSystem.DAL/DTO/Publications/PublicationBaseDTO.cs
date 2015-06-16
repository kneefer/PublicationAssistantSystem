using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using PublicationAssistantSystem.DAL.DTO.Misc;

namespace PublicationAssistantSystem.DAL.DTO.Publications
{
    [XmlInclude(typeof(ArticleDTO))]
    [XmlInclude(typeof(BookDTO))]
    [XmlInclude(typeof(ConferencePaperDTO))]
    [XmlInclude(typeof(DatasetDTO))]
    [XmlInclude(typeof(PatentDTO))]
    [XmlInclude(typeof(TechnicalReportDTO))]
    [XmlInclude(typeof(ThesisDTO))]
    [XmlType("Publication")]
    public abstract class PublicationBaseDTO
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public DateTime PublicationDate { get; set; }

        [XmlIgnore]
        public string Discriminator { get; set; }

        public bool IsComputing { get; set; }
        public bool IsOnWOS { get; set; }

        [XmlArray("Authors")]
        [XmlArrayItem("Author")]
        public List<EmployeeDTO> Employees { get; set; }
    }
}