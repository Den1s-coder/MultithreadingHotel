using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    class HotelRoom
    {
        private int RoomId { get; }
        private int SleepPlaces { get; set; }
        private int CostPerDay { get; set; }
        private bool Busy { get; set; }

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
