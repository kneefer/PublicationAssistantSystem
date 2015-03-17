using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ninject.Web.Common;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Repositories.Specific.Implementations;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;

namespace PublicationAssistantSystem.WebApi.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private readonly IKernel _kernel;
        public NinjectControllerFactory()
        {
            _kernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController) _kernel.Get(controllerType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IPublicationAssistantContext>().To<PublicationAssistantContext>().InRequestScope();

            _kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            _kernel.Bind<IJournalRepository>().To<JournalRepository>();
            _kernel.Bind<IJournalEditionRepository>().To<JournalEditionRepository>();
            _kernel.Bind<IDivisionRepository>().To<DivisionRepository>();
            _kernel.Bind<IFacultyRepository>().To<FacultyRepository>();
            _kernel.Bind<IInstituteRepository>().To<InstituteRepository>();
            _kernel.Bind<IPublicationBaseRepository>().To<PublicationRepository>();
        }
    }
}