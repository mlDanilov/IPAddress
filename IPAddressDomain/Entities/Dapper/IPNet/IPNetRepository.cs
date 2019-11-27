using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPAddressRepository.Classes;

namespace IPAddressRepository.Entities.Dapper.IPNet
{

    using IPNet = IPAddressRepository.Classes.IPNet;
    /// <summary>
    /// Репозиторий для работы с подсетями
    /// </summary>
    class IPNetRepository : IIPNetRepository
    {
        private readonly string _connString = string.Empty;

        public IPNetRepository(string connString_) {
            _connString = connString_;
        }

        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="user"></param>
        public void Create(IPNet ipNet_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                string sqlQuery = $"INSERT INTO IPNet (IPAddress, mask) " +
                    $"VALUES({ipNet_.Address.Address},{ipNet_.Mask})";
                db.Execute(sqlQuery);
            }
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="user"></param>
        public void Delete(IPNet ipNet_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = $"Delete from  IPNet where [id] = @Id";
                db.Execute(sqlQuery, ipNet_);
            }
        }

        /// <summary>
        /// Обновить клиента
        /// </summary>
        /// <param name="user"></param>
        public void Update(IPNet ipNet_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
            
                var p = new DynamicParameters();
                p.Add("@id", ipNet_.Id);
                p.Add("@IPAddress", ipNet_.Address.Address);
                p.Add("@mask", ipNet_.Mask);

                db.Query($"UpdateIPAddress", p, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Получить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IPNet Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var temp = db.Query<IpNet_fromDB>(
                    "SELECT * FROM IPNet WHERE Id = @id", new { id }).FirstOrDefault();

                return new IPNet()
                {
                    Id = temp.Id,
                    Address = new System.Net.IPAddress(temp.IPAddress),
                    Mask = temp.Mask
                };
            }
        }
        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <returns></returns>
        public List<IPNet> GetIPNets()
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var nets = db.Query<IpNet_fromDB>("SELECT * FROM IPNet").ToList();
                
                return nets.Select(net =>
                new IPNet()
                {
                    Id = net.Id,
                    Address = new System.Net.IPAddress(net.IPAddress),
                    Mask = net.Mask
                }).ToList();
            }
        }

        /// <summary>
        /// Получить все подсети клиента
        /// </summary>
        /// <param name="clientId_"></param>
        /// <returns></returns>
        public List<ClientIPNetExt> GetIPNetsByClientId(int clientId_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var p = new DynamicParameters();
                p.Add("@clientId", clientId_);

                var nets = db.Query<IpNet_fromDB>($"GetIPNetsByClientId", p, commandType: CommandType.StoredProcedure);

                return nets.Select(net =>
                new ClientIPNetExt()
                {
                    Id = net.Id,
                    Address = new System.Net.IPAddress(net.IPAddress),
                    ClientId = net.ClientId,
                    IpNetId = net.IpNetId,
                    Mask = net.Mask
                }).ToList();
            }
        }

        private struct IpNet_fromDB
        { 
            /// <summary>
            /// Уникальный ключ связи "клиент-подсеть"
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// Уникальный ключ клиента
            /// </summary>
            public int ClientId { get; set; }
            /// <summary>
            /// Уникальный ключ подсети
            /// </summary>
            public int IpNetId { get; set; }
            /// <summary>
            /// Ip-адресс подсети
            /// </summary>
            public long IPAddress { get; set; }
            /// <summary>
            /// Маска подсети
            /// </summary>
            public byte Mask { get; set; }
        }

    }
}
