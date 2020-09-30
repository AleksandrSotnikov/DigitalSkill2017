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
            dgView.ItemsSource = Manager.db.Users.ToList();
            cbOffices.ItemsSource = Manager.db.Offices.ToList();
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
            dgView.ItemsSource = Manager.db.Users.Where(n => n.Offices.Title == ((Offices)((ComboBox)sender).SelectedItem).Title.ToString()).ToList();
        }

        private void EditRole_Click(object sender, RoutedEventArgs e)
        {
            EditUser ed = new EditUser((Users)dgView.SelectedItem,this);
            ed.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void dgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (((Users)e.Row.Item).Roles.Title == "Administrator") e.Row.Background = new SolidColorBrush(Colors.Green);
            if (((Users)e.Row.Item).Active == false) e.Row.Background = new SolidColorBrush(Colors.Red);
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (((Users)dgView.SelectedItem) == null) return;
            ((Users)dgView.SelectedItem).Active = !((Users)dgView.SelectedItem).Active;
            Manager.db.SaveChanges();
            dgView.ItemsSource = Manager.db.Users.ToList();
        }

    }
}
