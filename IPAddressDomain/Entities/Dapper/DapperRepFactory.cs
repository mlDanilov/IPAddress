using IPAddressRepository.Entities.Dapper.Client;
using IPAddressRepository.Entities.Dapper.ClientIPNet;
using IPAddressRepository.Entities.Dapper.IPNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository.Entities.Dapper
{
    /// <summary>
    /// Фабрика, возвращающая репозитории 
    /// ORM - Dapper
    /// </summary>
    class DapperRepFactory : IRepFactory
    {

        private DapperRepFactory(string connectionString_) {
            _connectionString = connectionString_;
        }

        public static DapperRepFactory Get(string connectionString_) {
            if (_instance == null)
                _instance = new DapperRepFactory(connectionString_);
            return _instance;
        }
        private static DapperRepFactory _instance = null;

        readonly string _connectionString = string.Empty;
        /// <summary>
        /// Вернуть репозиторий для работы с клиентами
        /// </summary>
        /// <returns></returns>
        public IClientRepository GetClient()
        {
            return new ClientRepository(_connectionString);
        }
        /// <summary>
        /// Вернуть репозиторий для работы с "клиент-подсеть"
        /// </summary>
        /// <returns></returns>
        public IClientIPNetRepository GetClientIpNet()
        {
            return new ClientIPNetRepository(_connectionString);
        }
        /// <summary>
        /// Вернуть репозиторий для работы с подсетями
        /// </summary>
        /// <returns></returns>
        public IIPNetRepository GetIpNet()
        {
            return new IPNetRepository(_connectionString);
        }
    }
}
