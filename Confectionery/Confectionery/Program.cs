using ConfectioneryBusinessLogic.BusinessLogics;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryDataBaseImplement.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Confectionery
{
    internal static class Program
    {
        private static ServiceProvider? _serviceProvider;
        public static ServiceProvider? ServiceProvider => _serviceProvider;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
            Application.Run(_serviceProvider.GetRequiredService<FormMain>());
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging(option =>
            {
                option.SetMinimumLevel(LogLevel.Information);
                option.AddNLog("nlog.config");
            });
            services.AddTransient<IIngredientStorage, IngredientStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<IPastryStorage, PastryStorage>();
            services.AddTransient<IIngredientLogic, IngredientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IPastryLogic, PastryLogic>();
            services.AddTransient<FormMain>();
            services.AddTransient<FormIngredient>();
            services.AddTransient<FormIngredients>();
            services.AddTransient<FormCreateOrder>();
            services.AddTransient<FormPastry>();
            services.AddTransient<FormPastryIngredient>();
            services.AddTransient<FormPastries>();
        }
    }
}