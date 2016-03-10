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

        private const string FarDateFormat = "M";
        private const int SoonMovieBoundary = 15;

        public string AdditionalTimeString
        {
            get
            {
                var minutesLeft = Math.Ceiling((Time - DateTime.Now).TotalMinutes);
                if (minutesLeft <= SoonMovieBoundary && minutesLeft > 0)
                {
                    return string.Format(Resources.TimeLeftString, minutesLeft);
                }

                var isTomorrow = Time.Date == DateTime.Today + TimeSpan.FromDays(1);
                if (isTomorrow)
                {
                    return Resources.TomorrowText;
                }

                if (Time.Date == DateTime.Now.Date)
                {
                    return string.Empty;
                }

                return Time.ToString(FarDateFormat);
            }
        }

        public string ThreeDeeLabelText
        {
            get { return ThreeD ? Resources.ThreeDeeText : string.Empty; }
        }
    }
}