namespace DataAccess.Model
{
    public class AgeLimit
    {
        public int Id { get; set; }
        public string Limit { get; set; }

        public override string ToString()
        {
            return Limit + "+";
        }
    }
}