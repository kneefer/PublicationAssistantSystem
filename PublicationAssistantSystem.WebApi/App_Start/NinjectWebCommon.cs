using System;
using System.Web;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using PublicationAssistantSystem.DAL.Context;
using PublicationAssistantSystem.DAL.Repositories.Specific.Implementations;
using PublicationAssistantSystem.DAL.Repositories.Specific.Interfaces;
using PublicationAssistantSystem.WebApi;
using PublicationAssistantSystem.WebApi.Infrastructure;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace PublicationAssistantSystem.WebApi
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns> The created kernel. </returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectHttpResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Loads modules and register services.
        /// </summary>
        /// <param name="kernel"> The kernel. </param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPublicationAssistantContext>().To<PublicationAssistantContext>().InRequestScope();

            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>();
            kernel.Bind<IJournalRepository>().To<JournalRepository>();
            kernel.Bind<IJournalEditionRepository>().To<JournalEditionRepository>();
            kernel.Bind<IDivisionRepository>().To<DivisionRepository>();
            kernel.Bind<IFacultyRepository>().To<FacultyRepository>();
            kernel.Bind<IInstituteRepository>().To<InstituteRepository>();
            kernel.Bind<IPublicationBaseRepository>().To<PublicationRepository>();
        }        
    }
}
