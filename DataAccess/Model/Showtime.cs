using System;

namespace DataAccess.Model
{
    public class Showtime
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public Auditorium Auditorium { get; set; }
        public int Price { get; set; }
        public bool ThreeD { get; set; }

        public string AdditionalTimeString
        {
            get
            {
                var minutesLeft = (Time - DateTime.Now).TotalMinutes;
                if (minutesLeft <= 15 && minutesLeft > 0)
                {
                    return string.Format(Resources.TimeLeftString, minutesLeft);
                }

                var isTomorrow = DateTime.Today + TimeSpan.FromDays(1) == Time.Date;
                if (isTomorrow)
                {
                    return "завтра";
                }

                return "";
            }
        }

        public string ThreeDeeLabelText
        {
            get { return ThreeD ? "3D" : ""; }
        }
    }
}