using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    class HotelRoom
    {
        private int roomId;
        private int _nextRoomId;
        private int sleepPlaces;
        private int costPerDay;
        private bool busy = false;
    }
}
