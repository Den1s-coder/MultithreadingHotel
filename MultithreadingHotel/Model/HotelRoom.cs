using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    class HotelRoom
    {
        public int RoomId { get; }
        public int SleepPlaces { get; set; }
        public int CostPerDay { get; set; }
        public bool Busy { get; set; }

        private static int _nextRoomId = 1;

        public HotelRoom(int sleepPlaces, int costPerDay) 
        {
            RoomId = _nextRoomId++;
            SleepPlaces = sleepPlaces;
            CostPerDay = costPerDay;
            Busy = false;
        }
    }
}
