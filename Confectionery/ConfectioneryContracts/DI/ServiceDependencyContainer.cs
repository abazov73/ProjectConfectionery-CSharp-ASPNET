using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.DI
{
    public class ServiceDependencyContainer : IDependancyContainer
    {
        private ServiceProvider? _serviceProvider;

        private readonly ServiceCollection _serviceCollection;

        public ServiceDependencyContainer()
        {
            _serviceCollection = new ServiceCollection();
        }

        public void AddLogging(Action<ILoggingBuilder> configure)
        {
            _serviceCollection.AddLogging(configure);
        }

        public void RegisterType<T, U>(bool isSingle) where U : class, T where T : class
        {
            if (isSingle)
            {
                _serviceCollection.AddSingleton<T, U>();
            }
            else
            {
                _serviceCollection.AddTransient<T, U>();
            }
            _serviceProvider = null;
        }

        public void RegisterType<T>(bool isSingle) where T : class
        {
            if (isSingle)
            {
                _serviceCollection.AddSingleton<T>();
            }
            else
            {
                _serviceCollection.AddTransient<T>();
            }
            _serviceProvider = null;
        }

        public T Resolve<T>()
        {
            if (_serviceProvider == null)
            {
                _serviceProvider = _serviceCollection.BuildServiceProvider();
            }
            return _serviceProvider.GetService<T>()!;
        }
    }
}
