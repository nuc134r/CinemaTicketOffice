namespace DataAccess.Model
{
    public class Ticket
    {
        public int Id { get; set; }
        public Showtime Showtime { get; set; }
        public Seat Seat { get; set; }
        public bool IsUsed { get; set; }

        public Ticket Clone()
        {
            return new Ticket
            {
                Id = Id,
                Showtime = Showtime,
                Seat = Seat,
                IsUsed = IsUsed
            };
        }
    }
}