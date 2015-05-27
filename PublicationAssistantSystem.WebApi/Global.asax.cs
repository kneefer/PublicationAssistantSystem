using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using PublicationAssistantSystem.Core.Mappers.MNISW.Models;
using PublicationAssistantSystem.DAL.DTO.Publications;
using PublicationAssistantSystem.DAL.Models.Publications;
using PublicationAssistantSystem.WebApi.Infrastructure;

namespace PublicationAssistantSystem.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ConfigureMapper();
        }

        private void ConfigureMapper()
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
                .ForMember(dto => dto.JournalEditionId, conf => conf.MapFrom(ol => ol.Journal.Id));
            Mapper.CreateMap<Book, BookDTO>();
            Mapper.CreateMap<Dataset, DatasetDTO>();
            Mapper.CreateMap<ConferencePaper, ConferencePaperDTO>();
            Mapper.CreateMap<Patent, PatentDTO>();
            Mapper.CreateMap<TechnicalReport, TechnicalReportDTO>();
            Mapper.CreateMap<Thesis, ThesisDTO>();
        }
    }
}
