using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ConfectioneryContracts.DI
{
    public interface IDependancyContainer
    {
        /// <summary>
        /// Регистрация логгера
        /// </summary>
        /// <param name="configure"></param>
        void AddLogging(Action<ILoggingBuilder> configure);
        /// <summary>
        /// Добавление зависимости
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="U"></typeparam>
        /// <param name="isSingle"></param>
        void RegisterType<T, U>(bool isSingle) where U : class, T where T : class;
        /// <summary>
        /// Добавление зависимости
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="isSingle"></param>
        void RegisterType<T>(bool isSingle) where T : class;
        /// <summary>
        /// Получение класса со всеми зависмостями
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Resolve<T>();
    }
}
