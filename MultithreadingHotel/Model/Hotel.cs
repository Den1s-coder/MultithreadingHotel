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

        public static event Action<TouristLog> OnNewLog;

        public Hotel(int roomCount) 
        {
            for (int i = 0; i < roomCount; i++) 
            {
                Rooms.Add(new HotelRoom(2, 100)); 
            }
        }

        public void StartTouristFlow()
        {
            Thread thread = new Thread(() =>
            {
                Random random = new();

                while (true)
                {
                    Thread.Sleep(random.Next(500, 2000));

                    Tourist tourist = new Tourist(1, 2);

                    Thread checkInThread = new Thread(() => TryCheckIn(tourist));
                    checkInThread.Start();
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        private void TryCheckIn(Tourist tourist) 
        {
            lock (_lock) 
            {
                HotelRoom? freeRoom = Rooms.FirstOrDefault(r => !r.Busy && r.SleepPlaces >= tourist.TouristCount);
                if (freeRoom != null)
                {
                    DateTime checkIn = DateTime.Now;

                    freeRoom.Busy = true;

                    Thread checkOutThread = new Thread(() =>
                    {
                        Thread.Sleep(tourist.OrderedDays * 10000);
                        lock (_lock)
                        {
                            freeRoom.Busy = false;

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

                            OnNewLog?.Invoke(touristLog);
                        }
                    });
                    checkOutThread.Start();
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
