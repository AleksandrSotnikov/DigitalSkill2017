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
            dgView.ItemsSource = Manager.db.Schedules.Select(n => new {n.ID, n.Date, n.Time, Departure = n.Routes.Airports.IATACode, Arrival = n.Routes.Airports1.IATACode, n.FlightNumber, n.Aircrafts.MakeModel, n.EconomyPrice, Business = (n.EconomyPrice * (Decimal)1.3), First = (n.EconomyPrice * (Decimal)1.5) }).ToList();
            cbFrom.ItemsSource = Manager.db.Airports.Select(n => n.IATACode).ToList();
            cbTo.ItemsSource = Manager.db.Airports.Select(n => n.IATACode).ToList();
            cbNumber.ItemsSource = Manager.db.Schedules.Select(n => n.FlightNumber).Distinct().ToList();
            cbSort.ItemsSource = dgView.Columns.ToList().Select(n => n.Header).ToList();
        }

        //Не работает
        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgView.Columns[cbSort.SelectedIndex].SortDirection= ListSortDirection.Ascending;
            dgView_Sorting(dgView,new DataGridSortingEventArgs(dgView.Columns[2]));
            //dgView.Items.Refresh();
            // dgView_Sorting(dgView, new DataGridSortingEventArgs());
        }
        //Не работает
        private void dgView_Sorting(object sender, DataGridSortingEventArgs e)
        {
            this.Title = e.Column.Header.ToString();
            //this.Title = ((DataGrid)sender).CurrentItem.ToString() ;
            this.Title = sender.ToString();
        }    

        private void FlightEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgView.SelectedItem == null) return;
            ShadulesEdit se = new ShadulesEdit(dgView.SelectedItem.ToString(), this);
            se.Show();
        }

        

        private void FlightCancel_Click(object sender, RoutedEventArgs e)
        {
            if (dgView.SelectedItem == null) return;
            string schedules = dgView.SelectedItem.ToString().Substring(dgView.SelectedItem.ToString().IndexOf("=") + 2, dgView.SelectedItem.ToString().IndexOf(",")- dgView.SelectedItem.ToString().IndexOf("=") - 2);
            int _ID = int.Parse(schedules);
            Schedules _sc = Manager.db.Schedules.FirstOrDefault(n => n.ID == _ID);
            _sc.Confirmed = !_sc.Confirmed;
            Manager.db.SaveChanges();
        }

        private void dgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            string row = e.Row.Item.ToString().Substring(e.Row.Item.ToString().IndexOf("=") + 2, e.Row.Item.ToString().IndexOf(",")-e.Row.Item.ToString().IndexOf("=") -2 );
            int _ID = int.Parse(row);
            Schedules _sc = Manager.db.Schedules.FirstOrDefault(n => n.ID == _ID);
            e.Row.Background =_sc.Confirmed ? new SolidColorBrush(Colors.White):new SolidColorBrush(Colors.Red) ;  
        }

        private void SchedulesAdd_Click(object sender, RoutedEventArgs e)
        {
            new ShadulesAdd().Show();
        }
    }
}
