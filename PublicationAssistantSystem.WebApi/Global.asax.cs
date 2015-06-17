using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PublicationAssistantSystem.DAL.DTO.Misc;
using PublicationAssistantSystem.DAL.DTO.OrganisationUnits;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Misc;
using PublicationAssistantSystem.DAL.Models.OrganisationUnits;
using PublicationAssistantSystem.DAL.Models.Publications;

namespace PublicationAssistantSystem.WebApi
{
    /// <summary>
    /// Main application class.
    /// </summary>
    public class WebApiApplication : HttpApplication
    {
        /// <summary>
        /// Executed on application start.
        /// Initializes base settings of application.
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureMapper();
        }

        private static void ConfigureMapper()
        {
            #region Mappings for organisation units
            
            Mapper.CreateMap<FacultyDTO, Faculty>();
            Mapper.CreateMap<Faculty, FacultyDTO>();

            Mapper.CreateMap<InstituteDTO, Institute>();
            Mapper.CreateMap<Institute, InstituteDTO>();

            Mapper.CreateMap<DivisionDTO, Division>();
            Mapper.CreateMap<Division, DivisionDTO>();
            
            #endregion Mappings for organisation units
            
            #region Mappings for miscellaneous

            Mapper.CreateMap<EmployeeDTO, Employee>();
            Mapper.CreateMap<Employee, EmployeeDTO>();

            Mapper.CreateMap<JournalDTO, Journal>();
            Mapper.CreateMap<JournalPostDTO, Journal>();
            Mapper.CreateMap<Journal, JournalDTO>();

            Mapper.CreateMap<JournalEditionDTO, JournalEdition>();
            Mapper.CreateMap<JournalEdition, JournalEditionDTO>();

            #endregion Mappings for miscellaneous

            #region Mappings for publications

            Mapper.CreateMap<PublicationBase, PublicationBaseDTO>()
                .Include<Article, ArticleDTO>()
                .Include<Book, BookDTO>()
                .Include<Dataset, DatasetDTO>()
                .Include<ConferencePaper, ConferencePaperDTO>()
                .Include<Patent, PatentDTO>()
                .Include<TechnicalReport, TechnicalReportDTO>()
                .Include<Thesis, ThesisDTO>();

            Mapper.CreateMap<ArticleDTO, Article>();
            Mapper.CreateMap<Article, ArticleDTO>()
                .ForMember(dto=>dto.Discriminator, conf => conf.UseValue("Article"));

            Mapper.CreateMap<BookDTO, Book>();
            Mapper.CreateMap<Book, BookDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("Book"));

            Mapper.CreateMap<DatasetDTO, Dataset>();
            Mapper.CreateMap<Dataset, DatasetDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("Dataset"));

            Mapper.CreateMap<ConferencePaperDTO, ConferencePaper>();
            Mapper.CreateMap<ConferencePaper, ConferencePaperDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("ConferencePaper"));

            Mapper.CreateMap<PatentDTO, Patent>();
            Mapper.CreateMap<Patent, PatentDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("Patent"));

            Mapper.CreateMap<TechnicalReportDTO, TechnicalReport>();
            Mapper.CreateMap<TechnicalReport, TechnicalReportDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("TechnicalReport"));

            Mapper.CreateMap<ThesisDTO, Thesis>();
            Mapper.CreateMap<Thesis, ThesisDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.UseValue("Thesis"));
            
            #endregion Mappings for publications
        }
    }
}