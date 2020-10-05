using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для ManageFly.xaml
    /// </summary>
    public partial class ManageFly : Window
    {
        public ManageFly()
        {
            InitializeComponent();
            Manager.db = new dbDigitalSkills2017Entities();
            dgView.ItemsSource = Manager.db.Schedules.Select(n => new { n.Date, n.Time, Departure = n.Routes.Airports.IATACode, Arrival = n.Routes.Airports1.IATACode, n.FlightNumber, n.Aircrafts.MakeModel, n.EconomyPrice, Business = (n.EconomyPrice * (Decimal)1.3), First = (n.EconomyPrice * (Decimal)1.5) }).ToList();
            cbFrom.ItemsSource = Manager.db.Airports.Select(n => n.IATACode).ToList();
            cbTo.ItemsSource = Manager.db.Airports.Select(n => n.IATACode).ToList();
            cbNumber.ItemsSource = Manager.db.Schedules.Select(n => n.FlightNumber).Distinct().ToList();
            cbSort.ItemsSource = dgView.Columns.ToList().Select(n => n.Header).ToList();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgView.Columns[cbSort.SelectedIndex].SortDirection= ListSortDirection.Ascending;
            //dgView.Items.Refresh();
           // dgView_Sorting(dgView, new DataGridSortingEventArgs());
        }
    }
}
