using System;

namespace PublicationAssistantSystem.Core.Mappers.MNISW.Models
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class article : work
    {
        private journal journalField;
        private string issueField;
        private string volumeField;
        private string pagesField;
        /// 
        public journal journal
        {
            get
            {
                return this.journalField;
            }
            set
            {
                this.journalField = value;
            }
        }
        /// 
        public string issue
        {
            get
            {
                return this.issueField;
            }
            set
            {
                this.issueField = value;
            }
        }
        /// 
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }
        /// 
        public string pages
        {
            get
            {
                return this.pagesField;
            }
            set
            {
                this.pagesField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class journal
    {
        private string titleField;
        private string issnField;
        private string eissnField;
        private doi doiField;
        private string publishernameField;
        /// 
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        /// 
        public string issn
        {
            get
            {
                return this.issnField;
            }
            set
            {
                this.issnField = value;
            }
        }
        /// 
        public string eissn
        {
            get
            {
                return this.eissnField;
            }
            set
            {
                this.eissnField = value;
            }
        }
        /// 
        public doi doi
        {
            get
            {
                return this.doiField;
            }
            set
            {
                this.doiField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("publisher-name")]
        public string publishername
        {
            get
            {
                return this.publishernameField;
            }
            set
            {
                this.publishernameField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class doi
    {
        private string valueField;
        /// 
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// 
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(article))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(chapter))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(book))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public abstract partial class work
    {
        private string titleField;
        private contribution[] itemsField;
        private doi doiField;
        private workInstitution[] affiliationField;
        private string langField;
        private @abstract[] abstractField;
        private keywords[] keywordsField;
        private string publicationdateField;
        private conference conferenceField;
        private size sizeField;
        private string[] isField;
        private systemIdentifier systemidentifierField;
        /// 
        public string title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("author", typeof(author))]
        [System.Xml.Serialization.XmlElementAttribute("contributor", typeof(contributor))]
        [System.Xml.Serialization.XmlElementAttribute("editor", typeof(editor))]
        public contribution[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
        /// 
        public doi doi
        {
            get
            {
                return this.doiField;
            }
            set
            {
                this.doiField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("affiliation")]
        public workInstitution[] affiliation
        {
            get
            {
                return this.affiliationField;
            }
            set
            {
                this.affiliationField = value;
            }
        }
        /// 
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("abstract")]
        public @abstract[] @abstract
        {
            get
            {
                return this.abstractField;
            }
            set
            {
                this.abstractField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("keywords")]
        public keywords[] keywords
        {
            get
            {
                return this.keywordsField;
            }
            set
            {
                this.keywordsField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("publication-date")]
        public string publicationdate
        {
            get
            {
                return this.publicationdateField;
            }
            set
            {
                this.publicationdateField = value;
            }
        }
        /// 
        public conference conference
        {
            get
            {
                return this.conferenceField;
            }
            set
            {
                this.conferenceField = value;
            }
        }
        /// 
        public size size
        {
            get
            {
                return this.sizeField;
            }
            set
            {
                this.sizeField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("is")]
        public string[] @is
        {
            get
            {
                return this.isField;
            }
            set
            {
                this.isField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("system-identifier")]
        public systemIdentifier systemidentifier
        {
            get
            {
                return this.systemidentifierField;
            }
            set
            {
                this.systemidentifierField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class author : contribution
    {
    }
    /// 
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(contributor))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(editor))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(author))]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public abstract partial class contribution
    {
        private affiliation[] affiliationField;
        private string givennamesField;
        private string familynameField;
        private systemIdentifier systemidentifierField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("affiliation")]
        public affiliation[] affiliation
        {
            get
            {
                return this.affiliationField;
            }
            set
            {
                this.affiliationField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("given-names")]
        public string givennames
        {
            get
            {
                return this.givennamesField;
            }
            set
            {
                this.givennamesField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("family-name")]
        public string familyname
        {
            get
            {
                return this.familynameField;
            }
            set
            {
                this.familynameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("system-identifier")]
        public systemIdentifier systemidentifier
        {
            get
            {
                return this.systemidentifierField;
            }
            set
            {
                this.systemidentifierField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class affiliation
    {
        private string idField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "NCName")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class systemIdentifier
    {
        private string valueField;
        /// 
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class contributor : contribution
    {
        private contributorRole roleField;
        /// 
        public contributorRole role
        {
            get
            {
                return this.roleField;
            }
            set
            {
                this.roleField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = false)]
    public enum contributorRole
    {
        /// 
        translator,
        /// 
        reviewer,
        /// 
        supervisor,
        /// 
        undefined,
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class editor : contribution
    {
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class workInstitution
    {
        private string nameField;
        private systemIdentifier systemidentifierField;
        private string idField;
        /// 
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("system-identifier")]
        public systemIdentifier systemidentifier
        {
            get
            {
                return this.systemidentifierField;
            }
            set
            {
                this.systemidentifierField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(DataType = "NCName")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class @abstract
    {
        private string langField;
        private string valueField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class keywords
    {
        private string[] kField;
        private string langField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("k")]
        public string[] k
        {
            get
            {
                return this.kField;
            }
            set
            {
                this.kField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute(Form = System.Xml.Schema.XmlSchemaForm.Qualified, Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string lang
        {
            get
            {
                return this.langField;
            }
            set
            {
                this.langField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class conference
    {
        private string shortnameField;
        private string nameField;
        private string startdateField;
        private string enddateField;
        private string locationField;
        private string countryField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("short-name")]
        public string shortname
        {
            get
            {
                return this.shortnameField;
            }
            set
            {
                this.shortnameField = value;
            }
        }
        /// 
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("start-date")]
        public string startdate
        {
            get
            {
                return this.startdateField;
            }
            set
            {
                this.startdateField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("end-date")]
        public string enddate
        {
            get
            {
                return this.enddateField;
            }
            set
            {
                this.enddateField = value;
            }
        }
        /// 
        public string location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }
        /// 
        public string country
        {
            get
            {
                return this.countryField;
            }
            set
            {
                this.countryField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class size
    {
        private sizeUnit unitField;
        private decimal valueField;
        /// 
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public sizeUnit unit
        {
            get
            {
                return this.unitField;
            }
            set
            {
                this.unitField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlTextAttribute()]
        public decimal Value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    public enum sizeUnit
    {
        /// 
        sheets,
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class book : work
    {
        private string isbnField;
        private string seriesField;
        private string editionField;
        private string volumeField;
        private string publishernameField;
        private string publicationplaceField;
        /// 
        public string isbn
        {
            get
            {
                return this.isbnField;
            }
            set
            {
                this.isbnField = value;
            }
        }
        /// 
        public string series
        {
            get
            {
                return this.seriesField;
            }
            set
            {
                this.seriesField = value;
            }
        }
        /// 
        public string edition
        {
            get
            {
                return this.editionField;
            }
            set
            {
                this.editionField = value;
            }
        }
        /// 
        public string volume
        {
            get
            {
                return this.volumeField;
            }
            set
            {
                this.volumeField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("publisher-name")]
        public string publishername
        {
            get
            {
                return this.publishernameField;
            }
            set
            {
                this.publishernameField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlElementAttribute("publication-place")]
        public string publicationplace
        {
            get
            {
                return this.publicationplaceField;
            }
            set
            {
                this.publicationplaceField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = true)]
    public partial class chapter : work
    {
        private int chapternumberField;
        private bool chapternumberFieldSpecified;
        private book bookField;
        private string pagesField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("chapter-number")]
        public int chapternumber
        {
            get
            {
                return this.chapternumberField;
            }
            set
            {
                this.chapternumberField = value;
            }
        }
        /// 
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool chapternumberSpecified
        {
            get
            {
                return this.chapternumberFieldSpecified;
            }
            set
            {
                this.chapternumberFieldSpecified = value;
            }
        }
        /// 
        public book book
        {
            get
            {
                return this.bookField;
            }
            set
            {
                this.bookField = value;
            }
        }
        /// 
        public string pages
        {
            get
            {
                return this.pagesField;
            }
            set
            {
                this.pagesField = value;
            }
        }
    }
    /// 
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.34234")]
    [Serializable()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://pbn.nauka.gov.pl/-/ns/bibliography", IsNullable = false)]
    public partial class works
    {
        private work[] itemsField;
        /// 
        [System.Xml.Serialization.XmlElementAttribute("article", typeof(article))]
        [System.Xml.Serialization.XmlElementAttribute("book", typeof(book))]
        [System.Xml.Serialization.XmlElementAttribute("chapter", typeof(chapter))]
        public work[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
