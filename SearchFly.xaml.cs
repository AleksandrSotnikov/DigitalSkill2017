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
    /// Логика взаимодействия для SearchFly.xaml
    /// </summary>
    public partial class SearchFly : Window
    {
        public SearchFly()
        {
            InitializeComponent();
            Manager.db = new dbDigitalSkills2017Entities();
            FromComboBox.ItemsSource = Manager.db.Airports.ToList();
            ToComboBox.ItemsSource = Manager.db.Airports.ToList();
            CabinTypeComboBox.ItemsSource = Manager.db.CabinTypes.ToList();
        }
    }
}
