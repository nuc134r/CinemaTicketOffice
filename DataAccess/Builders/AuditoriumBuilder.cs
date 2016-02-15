namespace DataAccess.Builders
{
    public class AuditoriumBuilder
    {
        private int id;
        private string name;
        private int rows;
        private int seats;

        public AuditoriumBuilder WithId(int id)
        {
            this.id = id;
            return this;
        }

        public AuditoriumBuilder WithName(string name)
        {
            this.name = name;
            return this;
        }

        public AuditoriumBuilder WithRows(int rows)
        {
            this.rows = rows;
            return this;
        }

        public AuditoriumBuilder WithSeats(int seats)
        {
            this.seats = seats;
            return this;
        }
    }
}