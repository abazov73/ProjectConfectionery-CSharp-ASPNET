using ConfectioneryBusinessLogic.BusinessLogics;
using ConfectioneryBusinessLogic.OfficePackage.Implements;
using ConfectioneryBusinessLogic.OfficePackage;
using ConfectioneryContracts.BusinessLogicsContracts;
using ConfectioneryContracts.StoragesContracts;
using ConfectioneryDataBaseImplement.Implements;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using ConfectioneryBusinessLogic.MailWorker;
using ConfectioneryContracts.BindingModels;

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
            try
            {
                var mailSender = _serviceProvider.GetService<AbstractMailWorker>();
                mailSender?.MailConfig(new MailConfigBindingModel
                {
                    MailLogin = System.Configuration.ConfigurationManager.AppSettings["MailLogin"] ?? string.Empty,
                    MailPassword = System.Configuration.ConfigurationManager.AppSettings["MailPassword"] ?? string.Empty,
                    SmtpClientHost = System.Configuration.ConfigurationManager.AppSettings["SmtpClientHost"] ?? string.Empty,
                    SmtpClientPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["SmtpClientPort"]),
                    PopHost = System.Configuration.ConfigurationManager.AppSettings["PopHost"] ?? string.Empty,
                    PopPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PopPort"])
                });

                // создаем таймер
                var timer = new System.Threading.Timer(new TimerCallback(MailCheck!), null, 0, 100000);
            }
            catch (Exception ex)
            {
                var logger = _serviceProvider.GetService<ILogger>();
                logger?.LogError(ex, "Ошибка работы с почтой");
            }
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
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IImplementerStorage, ImplementerStorage>();
            services.AddTransient<IMessageInfoStorage, MessageInfoStorage>();
            services.AddTransient<IIngredientLogic, IngredientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IPastryLogic, PastryLogic>();
            services.AddTransient<IReportLogic, ReportLogic>();
            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IMessageInfoLogic, MessageInfoLogic>();
            services.AddTransient<IWorkProcess, WorkModeling>();
            services.AddTransient<IImplementerLogic, ImplementerLogic>();
            services.AddTransient<AbstractSaveToExcel, SaveToExcel>();
            services.AddTransient<AbstractSaveToWord, SaveToWord>();
            services.AddTransient<AbstractSaveToPdf, SaveToPdf>();
            services.AddSingleton<AbstractMailWorker, MailKitWorker>();
            services.AddTransient<FormMain>();
            services.AddTransient<FormIngredient>();
            services.AddTransient<FormIngredients>();
            services.AddTransient<FormCreateOrder>();
            services.AddTransient<FormPastry>();
            services.AddTransient<FormPastryIngredient>();
            services.AddTransient<FormPastries>();
            services.AddTransient<FormReportPastryIngredients>();
            services.AddTransient<FormReportOrders>();
            services.AddTransient<FormClients>();
            services.AddTransient<FormImplementers>();
            services.AddTransient<FormImplementer>();
            services.AddTransient<FormMails>();
        }

        private static void MailCheck(object obj) => ServiceProvider?.GetService<AbstractMailWorker>()?.MailCheck();
    }
}