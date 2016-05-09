using System;

namespace DataAccess
{
    public static class ObjectExtensions
    {
        public static int ToInt(this object o)
        {
            int value;
            var result = int.TryParse(o.ToString(), out value);

            if (!result)
            {
                return (int) double.Parse(o.ToString());
            }

            return value;
        }

        public static int? ToNulllableInt(this object o)
        {
            int value;
            var result = int.TryParse(o.ToString(), out value);

            if (!result)
            {
                return null;
            }

            return value;
        }

        public static DateTime ToDate(this object o)
        {
            return (DateTime) o;
        }

        public static bool ToBool(this object o)
        {
            return (bool) o;
        }

        public static object ThrowIfException(this object o)
        {
            var exception = o as Exception;
            if (exception != null) throw exception;

            return o;
        }
    }
}