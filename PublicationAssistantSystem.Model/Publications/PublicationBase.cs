using System;
using System.Collections.Generic;
using PublicationAssistantSystem.Model.Misc;

namespace PublicationAssistantSystem.Model.Publications
{
    public abstract class PublicationBase
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }

        public bool IsOnMNISZW { get; set; }
        public bool IsOnWOS { get; set; }
        public bool IsOnJCR { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}