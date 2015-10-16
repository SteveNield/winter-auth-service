using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Winter.Auth.Service.Models;

namespace Winter.Auth.Service.WindsorPlumbing
{
    public class ControllerInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
             .BasedOn<ApiController>()
             .LifestylePerWebRequest());

            container.Register(Component.For<IDtoValidator<LoginAttempt>>().ImplementedBy<LoginAttemptValidator>());
        }
    }
}