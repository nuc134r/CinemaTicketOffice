using System;

namespace DataAccess
{
    public static class ObjectExtensions
    {
        public static int ToInt(this object o)
        {
            return int.Parse(o.ToString());
        }

        public static DateTime ToDate(this object o)
        {
            return DateTime.Parse(o.ToString());
        }
    }
}