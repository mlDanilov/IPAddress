using IPAddressRepository.Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository
{
    /// <summary>
    /// Репозиторий для работы с клиентами
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Создать клиента
        /// </summary>
        /// <param name="user"></param>
        void Create(Client user);

        /// <summary>
        /// Удалить клиента
        /// </summary>
        /// <param name="user"></param>
        void Delete(int id);
        /// <summary>
        /// Обновить клиента
        /// </summary>
        /// <param name="user"></param>
        void Update(Client user);
        /// <summary>
        /// Получить клиента по уникальному коду
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Client Get(int id);
        /// <summary>
        /// Получить список клиентов
        /// </summary>
        /// <returns></returns>
        List<Client> GetClients();
    }
}