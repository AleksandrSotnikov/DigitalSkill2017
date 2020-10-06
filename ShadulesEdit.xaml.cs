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
        public ShadulesEdit(String schedules, ManageFly window)
        {
            InitializeComponent();
            _window = window;
            this.Title = schedules;
            schedules = schedules.Substring(schedules.IndexOf("=")+2);
            Date.Text = schedules.Substring(0,10);
            schedules = schedules.Substring(schedules.IndexOf("=")+2);
            Time.Text = schedules.Substring(0,8);
            schedules = schedules.Substring(schedules.IndexOf("=") + 2);
            From.Text = "From: "+ schedules.Substring(0, 3);
            schedules = schedules.Substring(schedules.IndexOf("=") + 2);
            To.Text =   "To: " + schedules.Substring(0, 3);
            schedules = schedules.Substring(schedules.IndexOf("=") + 2);
            schedules = schedules.Substring(schedules.IndexOf("=") + 2);
            Aircraft.Text = "Aircraft: " + schedules.Substring(0,schedules.IndexOf(","));
            schedules = schedules.Substring(schedules.IndexOf("=") + 2);
            Price.Text =schedules.Substring(0, schedules.IndexOf(","));

        }
    }
}
