using System;

namespace KioskClient
{
    public static class DateTimeExtensions
    {
        public static DateTime AM(this double value)
        {
            var hours = (int) value;
            var minutes = (value - hours) * 100;

            return DateTime.Today.AddHours(hours).AddMinutes(minutes);
        }

        public static DateTime PM(this double value)
        {
            var hours = (int) value;
            var minutes = (value - hours) * 100;

            return DateTime.Today.AddHours(hours + 12).AddMinutes(minutes);
        }

        public static DateTime of(this double value, int year)
        {
            var day = (int) value;
            var month = (int) ((value - day) * 100);

            return new DateTime(year, month, day);
        }


    }
}
