using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository.Entities
{
    /// <summary>
    /// Интерфейс фабрики, создающей репозитории
    /// </summary>
    interface IRepFactory
    {
        /// <summary>
        /// Получить репозиторий "Клинет - подсеть"
        /// </summary>
        /// <returns></returns>
        IClientIPNetRepository GetClientIpNet();
        /// <summary>
        /// Получить репозиторий "подсеть"
        /// </summary>
        /// <returns></returns>
        IIPNetRepository GetIpNet();
        /// <summary>
        /// Получить репозиторий "Клиент"
        /// </summary>
        /// <returns></returns>
        IClientRepository GetClient();

    }
}
