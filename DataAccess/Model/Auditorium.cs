namespace DataAccess.Model
{
    public class Auditorium
    {
        public Auditorium(int id, string name, int rows, int seats)
        {
            Id = id;
            Name = name;
            Rows = rows;
            Seats = seats;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Rows { get; private set; }
        public int Seats { get; private set; }
    }
}