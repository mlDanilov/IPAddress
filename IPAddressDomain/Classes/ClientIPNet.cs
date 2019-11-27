using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository.Classes
{
    /// <summary>
    /// Сущность клинет-подсеть
    /// </summary>
    public class ClientIPNet
    {
        public ClientIPNet()
        {

        }
        /// <summary>
        /// Уникальный код
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уникальный код клиента
        /// </summary>
        public int ClientId { get; set;}
        /// <summary>
        /// Уникальный код подсети
        /// </summary>
        public int IpNetId { get; set; }


    }


    public class ClientIPNetExt : ClientIPNet
    {
        /// <summary>
        /// IP-адресс подсети
        /// </summary>
        public IPAddress Address { get; set; }
        /// <summary>
        /// Маска
        /// </summary>
        public byte Mask { get; set; }
    }
}
