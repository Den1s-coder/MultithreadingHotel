using MultithreadingHotel.Model;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultithreadingHotel.View
{
    public partial class MainWindow : Window
    {
        public static LogWindow? LogWindow;

        private Hotel _hotel;

        public MainWindow()
        {
            InitializeComponent();
            _hotel = new Hotel(10);
            DataContext = _hotel;
            _hotel.StartTouristFlow();
        }

        private void ShowLog_Click(object sender, RoutedEventArgs e)
        {
            if (LogWindow == null)
            {
                LogWindow = new LogWindow();
                LogWindow.Closed += (s, args) => LogWindow = null;
                LogWindow.Show();
            }
        }
    }
}