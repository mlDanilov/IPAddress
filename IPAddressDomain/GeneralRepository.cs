using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPAddressRepository.Entities.Dapper;
using IPAddressRepository.Entities;

namespace IPAddressRepository
{
    //Реализуем принцип single responsibility

    /// <summary>
    /// Главный репозиторий
    /// </summary>
    public class GeneralRepository : IGeneralRepository
    {
        public  GeneralRepository(string connectionString_)
        {
            IRepFactory factory = DapperRepFactory.Get(connectionString_);
            
            ClientRep = factory.GetClient();
            IPNetRep = factory.GetIpNet();
            IPClientIPNetRep = factory.GetClientIpNet();
        }

        /// <summary>
        /// Репозиторий для работы с клиентами
        /// </summary>
        public IClientRepository ClientRep { get; private set; }
        /// <summary>
        /// Репозиторий для работы с подсетями
        /// </summary>
        public IIPNetRepository IPNetRep { get; private set; }
        /// <summary>
        /// Репозиторий для работы с "клиент-подсеть"
        /// </summary>
        public IClientIPNetRepository IPClientIPNetRep { get; private set; }
    }
}
