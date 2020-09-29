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
            dgView.ItemsSource = Manager.db.Users.Select(n => new {n.FirstName, n.LastName , n.Birthdate, n.Roles.Title, n.Email, OfficeTitle = n.Offices.Title }).ToList();  
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string[] headers = { "Имя", "Фамилия", "Дата Рождения", "Роль", "Email", "Офис" };
            Manager.fillDg(dgView, headers);
        }
    }
}
