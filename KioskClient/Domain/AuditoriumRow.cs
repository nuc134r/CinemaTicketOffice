using System.Collections.Generic;

namespace KioskClient.Domain
{
    public class AuditoriumRow
    {
        public int Number { get; set; }
        public List<AuditoriumSeat> Seats { get; set; }
    }
}