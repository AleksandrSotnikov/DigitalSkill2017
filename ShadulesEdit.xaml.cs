using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DigitalSkills2017
{
    /// <summary>
    /// Логика взаимодействия для ShadulesEdit.xaml
    /// </summary>
    public partial class ShadulesEdit : Window
    {
        ManageFly _window;
        Schedules _sc;
        int _ID;
        public ShadulesEdit(String schedules, ManageFly window)
        {
            InitializeComponent();
            _window = window;
            schedules = schedules.Substring(schedules.IndexOf("=")+2);
            _ID = int.Parse(schedules.Substring(0, schedules.IndexOf(",")));
            _sc =  Manager.db.Schedules.FirstOrDefault(n => n.ID == _ID);
            DataContext = _sc;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Price.Text = Price.Text.Substring(0, Price.Text.IndexOf("."));
            }
            catch { }
            _sc.EconomyPrice = decimal.Parse(Price.Text.ToString()); 
            _sc.Date = Convert.ToDateTime(Date.Text);
            _sc.Time = new TimeSpan(Convert.ToDateTime(Time.Text).Ticks%864000000000);
            Manager.db.SaveChanges();
            MessageBox.Show("Complete");
            _window.dgView.ItemsSource = Manager.db.Schedules.Select(n => new { n.ID, n.Date, n.Time, Departure = n.Routes.Airports.IATACode, Arrival = n.Routes.Airports1.IATACode, n.FlightNumber, n.Aircrafts.MakeModel, n.EconomyPrice, Business = (n.EconomyPrice * (Decimal)1.3), First = (n.EconomyPrice * (Decimal)1.5) }).ToList();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
