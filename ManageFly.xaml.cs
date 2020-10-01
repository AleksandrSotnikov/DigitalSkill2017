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
    /// Логика взаимодействия для ManageFly.xaml
    /// </summary>
    public partial class ManageFly : Window
    {
        public ManageFly()
        {
            InitializeComponent();
            Manager.db = new dbDigitalSkills2017Entities();
            dgView.ItemsSource = Manager.db.Schedules.ToList();
        }
    }
}
