using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    public class TouristLog
    {
        public int TouristId { get; set; }
        public int RoomId { get; set; }
        public int Days { get; set; }
        public DateTime CheckInTime { get; set; }
        public DateTime CheckOutTime { get; set; }
    }
}
