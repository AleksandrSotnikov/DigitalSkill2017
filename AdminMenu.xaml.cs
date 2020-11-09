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
        private static LoginUsers _loginUsers;
        public AdminMenu(LoginUsers loginUsers)
        {
            InitializeComponent();
            DateTime t = DateTime.Now;
            dgView.ItemsSource = Manager.db.Users.Select(n => new { n.FirstName, n.LastName, Age = (t.Month < n.Birthdate.Value.Month || (t.Month == n.Birthdate.Value.Month && t.Day < n.Birthdate.Value.Day))? (t.Year - n.Birthdate.Value.Year)-1: (t.Year - n.Birthdate.Value.Year), RolesTitles = n.Roles.Title, n.Email, OfficesTitles = n.Offices.Title, n.Active }).ToList();
            cbOffices.ItemsSource = Manager.db.Offices.ToList();
            _loginUsers = loginUsers;
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            _loginUsers.DateTimeExit = DateTime.Now;
            _loginUsers.Cause = "Good";
            Manager.db.SaveChanges();
            Close();
        }

        private void MenuItemAddUser_Click(object sender,RoutedEventArgs e)
        {
            new AddUser().Show();
        }

        private void cbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var t = DateTime.Now;
            dgView.ItemsSource = Manager.db.Users.Where(n => n.Offices.Title == ((Offices)((ComboBox)sender).SelectedItem).Title.ToString()).Select(n => new { n.FirstName, n.LastName, Age = (t.Month < n.Birthdate.Value.Month || (t.Month == n.Birthdate.Value.Month && t.Day < n.Birthdate.Value.Day)) ? (t.Year - n.Birthdate.Value.Year) - 1 : (t.Year - n.Birthdate.Value.Year), RolesTitles = n.Roles.Title, n.Email, OfficesTitles = n.Offices.Title, n.Active }).ToList();
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
            //if (((Users)e.Row.Item).Roles.Title == "Administrator") e.Row.Background = new SolidColorBrush(Colors.Green);
            //if (((Users)e.Row.Item).Active == false) e.Row.Background = new SolidColorBrush(Colors.Red); 
            e.Row.Background = new SolidColorBrush(Colors.White);
            if ((e.Row.Item.ToString().Contains("RolesTitles = Administrator"))) e.Row.Background = new SolidColorBrush(Colors.Green);
            if ((e.Row.Item.ToString().Contains("Active = False"))) e.Row.Background = new SolidColorBrush(Colors.Red);
           

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
