using System;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IPAddressRepository.Classes;

namespace IPAddressRepository.Entities.Dapper.Client
{

    using Client = IPAddressRepository.Classes.Client;
    /// <summary>
    /// Репозиторий для работы с клиентами
    /// </summary>
    class ClientRepository : IClientRepository
    {
        private readonly string  _connString = string.Empty;

        public ClientRepository(string connString_)
        {
            _connString = connString_;
        }
        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="user"></param>
        public void Create(Client user)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = "INSERT INTO Client (" +
                    "FirstName, SecondName, Patronymic, Birthday,sex) " +
                    "VALUES(@FirstName, @SecondName, @Patronymic, @Birthday, @Sex)";
                    db.Execute(sqlQuery, user);
            }
        }

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="user"></param>
        public void Delete(int clientId_)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var p = new DynamicParameters();
                p.Add("@clientId", clientId_);

                //var nets = db.Query<IpNet_fromDB>($"GetIPNetsByClientId", p, commandType: CommandType.StoredProcedure);
                db.Query($"DeleteClient", p, commandType: CommandType.StoredProcedure);
            }
        }


        /// <summary>
        /// Обновить клиента
        /// </summary>
        /// <param name="user"></param>
        public void Update(Client user)
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                var sqlQuery = $"update  Client " +
                    "set FirstName = @FirstName," +
                    "SecondName= @SecondName," +
                    "Patronymic = @Patronymic, " +
                    "BirthDay = @BirthDay, " +
                    "Sex = @Sex "+
                    $"where [id] = @Id";
                db.Execute(sqlQuery, user);
            }
        }
        /// <summary>
        /// Получить клиента
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Client Get(int id) 
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<Client>("SELECT * FROM Client WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }
        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <returns></returns>
        public List<Client> GetClients()
        {
            using (IDbConnection db = new SqlConnection(_connString))
            {
                return db.Query<Client>("SELECT * FROM Client").ToList();
            }
        }
    }
}
