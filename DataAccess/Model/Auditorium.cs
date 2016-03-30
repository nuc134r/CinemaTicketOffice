namespace DataAccess.Model
{
    public class Auditorium
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Rows { get; set; }
        public int Seats { get; set; }

        public int SeatCount
        {
            get { return Rows*Seats; }
        }

        public Auditorium Clone()
        {
            return new Auditorium
            {
                Id = Id,
                Name = Name,
                Rows = Rows,
                Seats = Seats
            };
        }
    }
}