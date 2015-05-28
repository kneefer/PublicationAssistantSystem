using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PublicationAssistantSystem.DAL.DTO.Publications;
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
            Mapper.CreateMap<PublicationBase, PublicationBaseDTO>()
                .Include<Article, ArticleDTO>()
                .Include<Book, BookDTO>()
                .Include<Dataset, DatasetDTO>()
                .Include<ConferencePaper, ConferencePaperDTO>()
                .Include<Patent, PatentDTO>()
                .Include<TechnicalReport, TechnicalReportDTO>()
                .Include<Thesis, ThesisDTO>();

            Mapper.CreateMap<Article, ArticleDTO>()
                .ForMember(dto => dto.JournalEditionId, conf => conf.MapFrom(ol => ol.Journal.Id))
                .ForMember(dto=>dto.Discriminator, conf => conf.MapFrom(ol => "Article"));
            Mapper.CreateMap<Book, BookDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "Book"));
            Mapper.CreateMap<Dataset, DatasetDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "Dataset"));
            Mapper.CreateMap<ConferencePaper, ConferencePaperDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "ConferencePaper"));
            Mapper.CreateMap<Patent, PatentDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "Patent"));
            Mapper.CreateMap<TechnicalReport, TechnicalReportDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "TechnicalReport"));
            Mapper.CreateMap<Thesis, ThesisDTO>()
                .ForMember(dto => dto.Discriminator, conf => conf.MapFrom(ol => "Thesis"));
        }
    }
}