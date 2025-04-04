using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultithreadingHotel.Model
{
    internal class Hotel
    {
        private List<HotelRoom> _rooms = new();
        private object _lock = new object();

        public Hotel(int roomCount) 
        {
            for (int i = 0; i < roomCount; i++) 
            {
                _rooms.Add(new HotelRoom(2, 100)); 
            }
        }

        public void StartTouristFlow() 
        {
            Task.Run(() => {
                Random random = new();

                while (true)
                {
                    Thread.Sleep(random.Next(5000, 10000));

                    Tourist tourist = new Tourist(2, 5);

                    Task.Run(() => TryCheckIn(tourist));
                }
            });
        }

        private void TryCheckIn(Tourist tourist) 
        {
            lock (_lock) 
            {
                HotelRoom? freeRoom = _rooms.FirstOrDefault(r => !r.Busy && r.SleepPlaces >= tourist.TouristCount);
                if (freeRoom != null)
                {
                    freeRoom.Busy = true;

                    Task.Run(() =>
                    {
                        Thread.Sleep(tourist.OrderedDays * 1000);
                        lock (_lock)
                        {
                            freeRoom.Busy = false;

                        }
                    });
                }
                else { }
            }
        }
            
    }
}
