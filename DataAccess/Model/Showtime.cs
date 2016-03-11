using System;

namespace DataAccess.Model
{
    public class Showtime
    {
        private const string FarDateFormat = "M";
        private const int SoonMovieBoundary = 30;
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public Auditorium Auditorium { get; set; }
        public int Price { get; set; }
        public bool ThreeD { get; set; }
        public bool ShowTimeLeft { get; set; }

        public string AdditionalTimeString
        {
            get
            {
                var minutesLeft = Math.Ceiling((Time - DateTime.Now).TotalMinutes);
                if (minutesLeft <= SoonMovieBoundary && minutesLeft > 0 && ShowTimeLeft)
                {
                    return string.Format(Resources.TimeLeftString, minutesLeft);
                }

                var isTomorrow = Time.Date == DateTime.Today + TimeSpan.FromDays(1);
                if (isTomorrow)
                {
                    if (Time.Hour < 6) return string.Empty;
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