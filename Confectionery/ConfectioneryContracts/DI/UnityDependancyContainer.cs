using Microsoft.Extensions.Logging;
using Unity.Microsoft.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace ConfectioneryContracts.DI
{
    public class UnityDependancyContainer : IDependancyContainer
    {
        private readonly UnityContainer _unityContainer;

        public UnityDependancyContainer()
        {
            _unityContainer = new UnityContainer();
        }
        public void AddLogging(Action<ILoggingBuilder> configure)
        {
            _unityContainer.AddExtension(new LoggingExtension(LoggerFactory.Create(configure)));
        }

        void IDependancyContainer.RegisterType<T, U>(bool isSingle)
        {
            if (isSingle) _unityContainer.RegisterSingleton<T, U>();
            else _unityContainer.RegisterType<T, U>();
        }

        public void RegisterType<T>(bool isSingle) where T : class
        {
            if (isSingle) _unityContainer.RegisterSingleton<T>();
            else _unityContainer.RegisterType<T>();
        }

        public T Resolve<T>()
        {
            return _unityContainer.Resolve<T>();
        }
    }
}
