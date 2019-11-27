using IPAddressRepository.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository
{
    /// <summary>
    /// Репозиторий для работы добавляения/удаления сетей клиентов
    /// </summary>
    public interface IClientIPNetRepository
    {
        /// <summary>
        /// Создать "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        void Create(ClientIPNet clientIPnet_);

        /// <summary>
        /// Создать "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        void Create(ClientIPNetExt clientIPnet_);

        /// <summary>
        /// Удалить "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        void Delete(int id_);

        /// <summary>
        /// Обновить "клиент-подсеть"
        /// </summary>
        /// <param name="user"></param>
        void Update(ClientIPNet clientIPnet_);
        /// <summary>
        /// Получить "клиент-подсеть"
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ClientIPNet Get(int id);
        /// <summary>
        /// Получить список "клиент-подсеть"
        /// </summary>
        /// <returns></returns>
        List<ClientIPNet> GetClientNets();
    }
}
