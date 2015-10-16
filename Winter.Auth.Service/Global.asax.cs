using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Winter.Auth.Repository;
using Winter.Auth.Service.WindsorPlumbing;
using Winter.Core.Auth;
using Winter.Core.DependencyInversion;

namespace Winter.Auth.Service
{
    public class WebApiApplication : HttpApplication
    {
        private IWindsorContainer _windsorContainer;

        private void BootstrapContainer()
        {
            var installers = new[]
            {
                FromAssembly.Containing<AuthInstaller>(),
                FromAssembly.Containing<ControllerInstaller>(),
                FromAssembly.Containing<RepositoryInstaller>(),
                FromAssembly.Containing<DependencyInversionInstaller>()
            };
            _windsorContainer = new WindsorContainer();
            _windsorContainer.Install(installers);
            _windsorContainer.Kernel.Resolver.AddSubResolver(new CollectionResolver(_windsorContainer.Kernel, true));
            GlobalConfiguration.Configuration.DependencyResolver = new WindsorResolver(_windsorContainer);
        }

        protected void Application_Start()
        {
            BootstrapContainer();
            GlobalConfiguration.Configure(c => WebApiConfig.Register(c, _windsorContainer));
        }
    }
}
