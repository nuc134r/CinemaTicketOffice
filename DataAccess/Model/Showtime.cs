using System;

namespace DataAccess.Model
{
    public class Showtime
    {
        public Showtime()
        {
            Time = DateTime.Now;
            ShowTimeLeft = true;
        }

        private const string FarDateFormat = "M";
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public DateTime Time { get; set; }
        public Auditorium Auditorium { get; set; }
        public int Price { get; set; }
        public bool ThreeDee { get; set; }
        public bool ShowTimeLeft { get; set; }

        public string AdditionalTimeString
        {
            get
            {
                var hours = Math.Ceiling((Time - DateTime.Now).TotalHours); 
                var minutes = Math.Ceiling((Time - DateTime.Now).TotalMinutes);
                
                var isTomorrow = hours > 24 && hours < 48;
                var isToday = hours < 24;
                
                if (hours < 6)
                {
                    if (minutes < 60)
                    {
                        return string.Format(Resources.TimeLeftString, minutes);
                    }
                    else
                    {
                        return string.Format("осталось {0} ч {1} мин", (int)(minutes / 60), minutes % 60);
                    }
                }
                else if (isToday)
                {
                    return string.Empty;
                }
                else if (isTomorrow)
                {
                    return Resources.TomorrowText;
                }
                
                return Time.ToString(FarDateFormat);
            }
        }

        public string ThreeDeeLabelText
        {
            get { return ThreeDee ? Resources.ThreeDeeText : string.Empty; }
        }

        public Showtime Clone()
        {
            return new Showtime
            {
                Movie = Movie,
                Id = Id,
                Time = Time,
                ThreeDee = ThreeDee,
                Auditorium = Auditorium,
                Price = Price,
                ShowTimeLeft = ShowTimeLeft
            };
        }
    }
}