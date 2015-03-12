using System;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using PublicationAssistantSystem.DAL.Context;

namespace PublicationAssistantSystem.Web.Infrastructure
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
            _kernel.Bind<IPublicationRepository>().To<EFPublicationRepository>();
        }
    }
}