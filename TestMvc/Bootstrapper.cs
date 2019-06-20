using System;
using Castle.Windsor;
using Siemens.Crosscutting.Utilities.Installer;

namespace TestMvc
{
    public sealed class Bootstrapper : IDisposable
    {
        private IWindsorContainer _container;

        public Bootstrapper()
        {
            _container = new WindsorContainer();
            _container.Install(new GeneralInstaller());
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void Release(object resolvedType)
        {
            _container.Release(resolvedType);
        }

        public void Dispose()
        {
            if (_container == null)
            {
                return;
            }

            _container.Dispose();
            _container = null;
        }
    }
}