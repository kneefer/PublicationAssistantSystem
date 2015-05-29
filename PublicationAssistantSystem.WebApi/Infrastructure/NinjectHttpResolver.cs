using System.Web.Http.Dependencies;
using Ninject;

namespace PublicationAssistantSystem.WebApi.Infrastructure
{
    /// <summary>
    /// Dependency injection http resolver.
    /// </summary>
    public class NinjectHttpResolver : NinjectScope, IDependencyResolver
    {
        private IKernel _kernel;
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="kernel"> Super-factory object that can create any other objects. </param>
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