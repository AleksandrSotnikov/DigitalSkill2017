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
    /// Логика взаимодействия для AdminMenu.xaml
    /// </summary>
    public partial class AdminMenu : Window
    {
        public AdminMenu()
        {
            InitializeComponent();
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            Close();
        }

        private void MenuItemAddUser_Click(object sender,RoutedEventArgs e)
        {
            new AddUser().Show();
        }
    }
}
