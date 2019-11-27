using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository
{
    //Реализуем принцип single responsibility

    /// <summary>
    /// Главный репозиторий
    /// </summary>
    public interface IGeneralRepository
    {

        /// <summary>
        /// Репозиторий для работы с клиентами
        /// </summary>
        IClientRepository ClientRep { get; }

        /// <summary>
        /// Репозиторий для работы подсетями
        /// </summary>
        IIPNetRepository IPNetRep { get; }

        /// <summary>
        /// Репозиторий для работы со связью "клиент-подсеть"
        /// </summary>
        IClientIPNetRepository IPClientIPNetRep { get; }

    }
}
