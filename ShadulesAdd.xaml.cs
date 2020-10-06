using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для ShadulesAdd.xaml
    /// </summary>
    public partial class ShadulesAdd : Window
    {
        public ShadulesAdd()
        {
            InitializeComponent();
        }

        private void ImportChangesButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog oFD = new OpenFileDialog();
            oFD.Title = "Открытие файла";
            oFD.Filter = "csv (*.csv)|*.csv|All files (*.*)|*.*";
            oFD.RestoreDirectory = true;
            if (oFD.ShowDialog() == true)
            {
                StreamReader fileIn = new StreamReader(new FileStream(oFD.FileName.ToString(), FileMode.Open, FileAccess.Read));
                string[] s = fileIn.ReadToEnd().Split('\n');
                int miss = 0;
                for(int i = 0; i < s.Length; i++)
                {
                    string[] q = s[i].Split(',');
                    if (q[0] == "ADD")
                    {                      
                        try
                        {
                            Schedules schedules = new Schedules();
                            schedules.Date = Convert.ToDateTime(q[1]);
                            schedules.Time = new TimeSpan(Convert.ToDateTime(q[2]).Ticks % 864000000000);
                            schedules.FlightNumber = q[3];
                            var q1 = q[4].ToString();
                            var q2 = q[5].ToString();
                            int airports = Manager.db.Airports.FirstOrDefault(n => n.IATACode == q1).ID;
                            int airports1 = Manager.db.Airports.FirstOrDefault(n => n.IATACode == q2).ID;
                            Routes routes = Manager.db.Routes.FirstOrDefault(n => n.DepartureAirportID == airports && n.ArrivalAirportID == airports1);
                            schedules.RouteID = routes.ID;
                            schedules.AircraftID = Convert.ToInt32(q[6]);
                            q[7] = q[7].Substring(0, q[7].IndexOf('.'));
                            schedules.EconomyPrice = Convert.ToDecimal(q[7]);
                            schedules.Confirmed = (q[8].Contains("OK"));
                            Manager.db.Schedules.Add(schedules);
                        }
                        catch
                        {
                            miss++;
                        }

                    }
                    if (q[0] == "EDIT")
                    {
                        DateTime dt = Convert.ToDateTime(q[1]);
                        TimeSpan ts = new TimeSpan(Convert.ToDateTime(q[2]).Ticks % 864000000000);
                        Schedules schedules = Manager.db.Schedules.FirstOrDefault(n => n.Date == dt&&n.Time==ts);
                        schedules.Confirmed = !schedules.Confirmed;
                    }
                }
                Manager.db.SaveChanges();
                RecordTextBlock.Text = miss.ToString();
                fileIn.Close();
            }
        }
    }
}
