using IPAddressRepository.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository
{
    /// <summary>
    /// Репозиторий для работы с подсетями
    /// </summary>
    public interface IIPNetRepository
    {
        /// <summary>
        /// Добавить подсеть
        /// </summary>
        /// <param name="ipNet_"></param>
        void Create(IPNet ipNet_);

        /// <summary>
        /// Удалить подсеть
        /// </summary>
        /// <param name="user"></param>
        void Delete(IPNet ipNet_);

        /// <summary>
        /// Обновить подсеть
        /// </summary>
        /// <param name="user"></param>
        void Update(IPNet ipNet_);
        /// <summary>
        /// Получить подесть
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IPNet Get(int id);
        /// <summary>
        /// Получить список доступных подсетей
        /// </summary>
        /// <returns></returns>
        List<IPNet> GetIPNets();

        /// <summary>
        /// Получить все подсети клиента
        /// </summary>
        /// <param name="clientId_"></param>
        /// <returns></returns>
        List<ClientIPNetExt> GetIPNetsByClientId(int clientId_);
    }
}
