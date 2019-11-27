using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository.Classes
{
    /// <summary>
    /// Подсеть
    /// </summary>
    public class IPNet
    {
        /// <summary>
        /// Уникальный код
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// IP-адресс подсети
        /// </summary>
        public IPAddress Address { get; set; }
        /// <summary>
        /// Маска
        /// </summary>
        public short Mask { get; set; }
    }
    /// <summary>
    /// Связь "клиент-подсеть" с полями.
    /// Класс адаптирован для работе во FrontEnd'е
    /// </summary>
    public class ClientIPNetFront
    {
        /// <summary>
        /// Уникальный код  "клиент-подсеть"
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Уникальный код клиента
        /// </summary>
        public int ClientId { get; set; }
        /// <summary>
        /// Уникальный код подсети
        /// </summary>
        public int IpNetId { get; set; }
        #region IP Address
        /// <summary>
        /// 1 октет IP адреса
        /// </summary>
        [Range(0, 255)]
        public byte Oktet1 { get; set; }
        /// <summary>
        /// 2 октет IP адреса
        /// </summary>
        [Range(0, 255)]
        public byte Oktet2 { get; set; }
        /// <summary>
        /// 3 октет IP адреса
        /// </summary>
        [Range(0, 255)]
        public byte Oktet3 { get; set; }
        /// <summary>
        /// 4 октет IP адреса
        /// </summary>
        [Range(0, 255)]
        public byte Oktet4 { get; set; }
        #endregion
        /// <summary>
        /// Маска
        /// </summary>
        [Range(0, 32)]
        public byte Mask { get; set; }
    }
}
