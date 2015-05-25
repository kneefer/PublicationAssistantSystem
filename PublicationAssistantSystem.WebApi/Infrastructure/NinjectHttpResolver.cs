using System.Web.Http.Dependencies;
using Ninject;

namespace PublicationAssistantSystem.WebApi.Infrastructure
{

    public class NinjectHttpResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;
        public NinjectHttpResolver(IKernel kernel)
            : base(kernel)
        {
            _kernel = kernel;
        }
        public IDependencyScope BeginScope()
        {
            return new NinjectScope(_kernel.BeginBlock());
        }
    }
}