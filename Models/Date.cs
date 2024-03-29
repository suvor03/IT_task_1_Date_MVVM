using System;

namespace WpfApplicationData.Models
{
    public class Date
    {
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public Date(int day, int month, int year)
        {
            if (day < 1)
            {
                throw new ArgumentException("Значение дня не может быть меньше 1");
            }
            if (month < 1 || month > 12)
            {
                throw new ArgumentException("Значение месяца должно быть от 1 до 12");
            }
            if (year < 1)
            {
                throw new ArgumentException("Значение года не может быть меньше 1");
            }

            Day = day;
            Month = month;
            Year = year;
        }

        public int CalculateDaysInMonth()
        {
            return Month switch
            {
                2 => IsLeapYear() ? 29 : 28,
                4 or 6 or 9 or 11 => 30,
                _ => 31
            };
        }

        public string IsDateValid()
        {
            if (Year < 1)
            {
                return "Неверное значение года. Исчисление начинается с 1 г. н.э.";
            }

            if (Month < 1 || Month > 12)
            {
                return "Неверное значение месяца. В году 12 месяцев.";
            }
            
            if (Day < 1 || Day > CalculateDaysInMonth())
            {
                return "Неверное значение дней (для данного года и месяца).";
            }

            return "Корректная дата!";
        }

        public void AddDaysMonthsYears(int days = 0, int months = 0, int years = 0)
        {
            if (days < 0 || months < 0 || years < 0)
            {
                throw new ArgumentOutOfRangeException("Вы ввели отрицательное значение. Дату можно только увеличивать.");
            }
            
            Day += days;
            Month += months;
            Year += years;

            var daysInMonth = CalculateDaysInMonth();
            Month += (Day - 1) / daysInMonth;
            Day = (Day - 1) % daysInMonth + 1;

            Year += (Month - 1) / 12;
            Month = (Month - 1) % 12 + 1;
        }

        private bool IsLeapYear()
        {
            return (Year % 4 == 0 && Year % 100 != 0) || (Year % 400 == 0);
        }
    }
}