using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IPAddressRepository.Validators
{
    /// <summary>
    /// Валидатор задаёт допустимый диапазон для дня рождения клиента
    /// </summary>
    public class BirthDayAttribute : ValidationAttribute
    {
        private readonly DateTime _bDate;
        private readonly DateTime _eDate;
        public BirthDayAttribute(string bDateStr_, string eDateStr_) {
            ErrorMessage = "День рождения клиента за рамками диапазона";
            _bDate = DateTime.Parse(bDateStr_);
            _eDate = DateTime.Parse(eDateStr_);
        }
        public override bool IsValid(object value)
        {
            var birthDay = Convert.ToDateTime(value);
            return (birthDay >= _bDate && birthDay <= _eDate);
             
        }

    }
}
