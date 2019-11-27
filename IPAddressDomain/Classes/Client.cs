using IPAddressRepository.Services;
using IPAddressRepository.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPAddressRepository.Classes
{
    /// <summary>
    /// Клиент
    /// </summary>
    public class Client
    {
        public Client() {

        }
        /// <summary>
        /// Уникальный код
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        [RegularExpression(@"(^[А-Я]{1}[а-я]+)|(^[A-Z]{1}[a-z]+)")]
        public string FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [RegularExpression(@"(^[А-Я]{1}[а-я]+)|(^[A-Z]{1}[a-z]+)")]
        public string SecondName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [RegularExpression(@"(^[А-Я]{1}[а-я]+)|(^[A-Z]{1}[a-z]+)")]
        public string Patronymic { get; set; }
        /// <summary>
        /// День рождения
        /// </summary>
        [BirthDay("1900-01-01", "2002-01-01")]
        public DateTime BirthDay { get; set; }

        /// <summary>
        /// Пол
        /// </summary>
        public Sex Sex {get; set;}
           

    }
}
