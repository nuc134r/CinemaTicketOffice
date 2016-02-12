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
    }
}
