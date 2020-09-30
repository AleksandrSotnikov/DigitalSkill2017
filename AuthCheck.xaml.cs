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
    /// Логика взаимодействия для AuthCheck.xaml
    /// </summary>
    public partial class AuthCheck : Window
    {
        public AuthCheck()
        {
            InitializeComponent();
            dgView.ItemsSource = Manager.db.LoginUsers.ToList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void dgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (((LoginUsers)e.Row.Item).Cause == "Soft") e.Row.Background = new SolidColorBrush(Colors.Red);
            if (((LoginUsers)e.Row.Item).Cause == "System") e.Row.Background = new SolidColorBrush(Colors.Green);

        }

        private void Soft_Checked(object sender, RoutedEventArgs e)
        {
            dgView.ItemsSource = Manager.db.LoginUsers.Where(n => n.Cause == "Soft").ToList();
        }

        private void System_Checked(object sender, RoutedEventArgs e)
        {
            dgView.ItemsSource = Manager.db.LoginUsers.Where(n => n.Cause == "System").ToList();
        }
    }
}
