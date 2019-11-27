using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAddressRepository.Services
{
    public static class BirthDayService
    {
        /// <summary>
        /// Начальная допустимая дата дня рождения клиента
        /// </summary>
        public static string BeginDate
        {
            get
            {
                return new DateTime(1900, 1, 1).ToShortDateString();
            }
        }
        /// <summary>
        /// Конечная допустимая дата дня рождения клинета
        /// </summary>
        public static string EndDate
        {
            get
            {
                return DateTime.Now.AddYears(-18).ToShortDateString();
            }
        }

        private static string getEndDate()
        {
            var datetime = new DateTime(1900, 1, 1);
            return datetime.ToShortDateString();
        }
    }
}
