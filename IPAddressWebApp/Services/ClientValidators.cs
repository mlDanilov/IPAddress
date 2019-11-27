using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IPAddressWebApp.Services
{
    public static class ClientValidators
    {

        public static string BeginDate
        {
            get
            {
                var datetime = new DateTime(1900, 1, 1);
                return datetime.ToShortDateString();
            }
        }
        public static string EndDate
        {
            get
            {
                var datetime = DateTime.Now.AddYears(-18);
                return datetime.ToShortDateString();
            }
        }

        private static string getEndDate()
        {
            var datetime = new DateTime(1900, 1, 1);
            return datetime.ToShortDateString();
        }
    }
}
