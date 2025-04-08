using MultithreadingHotel.Model;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace MultithreadingHotel.View
{
    public partial class LogWindow : Window
    {
        private ObservableCollection<TouristLog> _logs = new();

        public LogWindow()
        {
            InitializeComponent();
            LogDataGrid.ItemsSource = _logs;

            LoadLogs();

            Hotel.OnNewLog += AddLogEntry;
        }

        private void AddLogEntry(TouristLog log)
        {
            Dispatcher.Invoke(() => _logs.Add(log));
        }

        private void LoadLogs()
        {
            string filePath = "TouristLog.json";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var entries = JsonSerializer.Deserialize<List<TouristLog>>(json);
                if (entries != null)
                {
                    foreach (var entry in entries)
                        _logs.Add(entry);
                }
            }
        }
    }
}
