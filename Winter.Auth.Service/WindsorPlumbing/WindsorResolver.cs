using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;

namespace Winter.Auth.Service.WindsorPlumbing
{
    public class WindsorResolver : IDependencyResolver
    {
        private IWindsorContainer _container;

        public WindsorResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? 
                _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return !_container.Kernel.HasComponent(serviceType) ? 
                new object[0] : _container.ResolveAll(serviceType).Cast<object>();
        }

        public void Dispose()
        {
            _container = null;
        }
    }
}