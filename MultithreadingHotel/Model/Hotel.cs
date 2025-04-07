using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MultithreadingHotel.Model
{
    internal class Hotel
    {
        public ObservableCollection<HotelRoom> Rooms { get; } = new();
        private object _lock = new object();
        private Dispatcher _dispatcher;

        public Hotel(int roomCount, Dispatcher dispatcher) 
        {
            _dispatcher = dispatcher;
            for (int i = 0; i < roomCount; i++) 
            {
                Rooms.Add(new HotelRoom(2, 100)); 
            }
        }

        public void StartTouristFlow() 
        {
            Task.Run(() => {
                Random random = new();

                while (true)
                {
                    Thread.Sleep(random.Next(1000, 3000));

                    Tourist tourist = new Tourist(1, 2);

                    Task.Run(() => TryCheckIn(tourist));
                }
            });
        }

        private void TryCheckIn(Tourist tourist) 
        {
            lock (_lock) 
            {
                HotelRoom? freeRoom = Rooms.FirstOrDefault(r => !r.Busy && r.SleepPlaces >= tourist.TouristCount);
                if (freeRoom != null)
                {
                    DateTime checkIn = DateTime.Now;

                    _dispatcher.Invoke(() => freeRoom.Busy = true);

                    freeRoom.Busy = true;

                    Task.Run(() =>
                    {
                        Thread.Sleep(tourist.OrderedDays * 5000);
                        lock (_lock)
                        {
                            _dispatcher.Invoke(() => freeRoom.Busy = false);

                            DateTime checkOut = DateTime.Now;
                            TouristLog touristLog = new() 
                            {
                                TouristId = tourist.OrderId,
                                RoomId = freeRoom.RoomId,
                                Days = tourist.OrderedDays,
                                CheckInTime = checkIn,
                                CheckOutTime = checkOut
                            };

                            LogToJson(touristLog);
                        }
                    });
                }
                else { }
            }
        }

        private void LogToJson(TouristLog log)
        {
            string fileName = $"TouristLog.json";
            List<TouristLog> logs = new();

            if (File.Exists(fileName)) 
            {
                string json = File.ReadAllText(fileName);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    logs = JsonSerializer.Deserialize<List<TouristLog>>(json) ?? new();
                }
            }

            logs.Add(log);

            string newJson = JsonSerializer.Serialize(logs, new JsonSerializerOptions { WriteIndented = true});
            File.WriteAllText(fileName, newJson);
        }
            
    }
}
