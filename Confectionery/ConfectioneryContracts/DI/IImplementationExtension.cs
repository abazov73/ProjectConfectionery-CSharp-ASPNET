using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfectioneryContracts.DI
{
    public interface IImplementationExtension
    {
        public int Priority { get; }
        /// <summary>
        /// Регистрация сервисов
        /// </summary>
        public void RegisterServices();
    }
}
