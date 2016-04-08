namespace DataAccess.Model
{
    public class Seat
    {
        public int SeatNumber { get; set; }
        public int RowNumber { get; set; }

        public string SeatString
        {
            get { return string.Format(Resources.SeatText, RowNumber, SeatNumber); }
        }
    }
}