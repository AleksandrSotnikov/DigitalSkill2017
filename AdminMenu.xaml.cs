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
            cbOffice.ItemsSource = Manager.db.Offices.Select(n => n.Title).ToList();
            dgView.ItemsSource = Manager.db.Users.ToList();
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

        private void cbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dgView.ItemsSource = Manager.db.Users.Where(n=> cbOffice.SelectedItem==n.Offices.Title).ToList();
        }

        private void EditRole_Click(object sender, RoutedEventArgs e)
        {
            EditUser ed = new EditUser((Users)dgView.SelectedItem);
            ed.Show();
            //MessageBox.Show(((Users)dgView.SelectedItem).Email.ToString());
        }
    }
}
