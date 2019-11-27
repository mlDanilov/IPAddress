using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPAddressRepository.Classes;


namespace IPAddressRepository.Entities.Dapper.ClientIPNet
{

    using ClientIPNet = IPAddressRepository.Classes.ClientIPNet;
    /// <summary>
    /// Репозиторий для работы добавляения/удаления "клиент-подсеть"
    /// </summary>
    class ClientIPNetRepository :IClientIPNetRepository
    {

        private readonly string _connString = string.Empty;
        public ClientIPNetRepository(string connString_)
        {
            
            _connString = connString_;
        }

        /// <summary>
        /// Создать "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        public void Create(ClientIPNet clientIPnet_) 
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = "INSERT INTO Client_IPNet (" +
                    "ClientId, IpNetId) " +
                    "VALUES(@ClientId, @IpNetId)";
                db.Execute(sqlQuery, clientIPnet_);
            }
        }
        
        public void Create(ClientIPNetExt clientIPnet_)
        {
            
            using (IDbConnection db = new SqlConnection(_connString))
            {
                //var sqlQuery = "CreateIPAddress";
                //db.Execute(sqlQuery, clientIPnet_);
                var p = new DynamicParameters();
                p.Add("@ClientId", clientIPnet_.ClientId);
                p.Add("@IPAddress", clientIPnet_.Address.Address);
                p.Add("@mask", clientIPnet_.Mask);

                db.Query($"CreateIPAddress", p, commandType: CommandType.StoredProcedure);

            }
           
        }

        /// <summary>
        /// Удалить "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        public void Delete(int id_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = $"Delete from  Client_IPNet where [id] = {id_}";
                db.Execute(sqlQuery);
            }
        }

        /// <summary>
        /// Обновить "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        public void Update(ClientIPNet clientIPnet_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = $"update  Client_IPNet " +
                    "set ClientID = @ClientId, " +
                    "IpNetId= @IpNetId " +
                    $"where [id] = @Id";
                db.Execute(sqlQuery, clientIPnet_);
            }
        }
        /// <summary>
        /// Получить "клиент-подсеть"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientIPNet Get(int id)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<ClientIPNet>("SELECT * FROM Client_IPNet WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Получить список "клиент-подсеть"
        /// </summary>
        /// <returns></returns>
        public List<ClientIPNet> GetClientNets()
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<ClientIPNet>("SELECT * FROM Client_IPNet").ToList();
            }
        }
    }
}
