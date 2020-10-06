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
                for(int i = 0; i < s.Length; i++)
                {
                    string[] q = s[i].Split(',');
                    if (q[0] == "ADD")
                    {
                        //- это действие, дата отправления, время
                       // отправления, номер рейса, международный код аэропорта отправления, международный код
//аэропорта прибытия, код самолета, базовая цена и подтверждение.
                        Schedules schedules = new Schedules();
                        schedules.Date =q[1];
                        schedules.Time = q[2];
                        schedules.Routes.Airports.IATACode = q[3];
                        schedules.Routes.Airports1.IATACode = q[4];
                        schedules.AircraftID = q[5];
                        schedules.EconomyPrice = q[6];
                        if (q[7] == "OK")
                        {
                            schedules.Confirmed = true;
                        }
                        else
                        {
                            schedules.Confirmed = false;
                        }

                    }
                    if (q[0] == "EDIT")
                    {

                    }
                }
                fileIn.Close();
            }
        }
    }
}
