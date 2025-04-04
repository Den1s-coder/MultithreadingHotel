using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace MultithreadingHotel.Model
{
    class HotelRoom:INotifyPropertyChanged
    {
        public int RoomId { get; }
        public int SleepPlaces { get; set; }
        public int CostPerDay { get; set; }
        public bool Busy 
        {
            get => _isBusy;   
            set { _isBusy = value;
                OnPropertyChanged(nameof(Busy));
            } 
        }

        private bool _isBusy;
        private static int _nextRoomId = 1;

        public event PropertyChangedEventHandler? PropertyChanged;
        

        public HotelRoom(int sleepPlaces, int costPerDay) 
        {
            RoomId = _nextRoomId++;
            SleepPlaces = sleepPlaces;
            CostPerDay = costPerDay;
            Busy = false;
        }

        public void OnPropertyChanged(string propertyName) 
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
