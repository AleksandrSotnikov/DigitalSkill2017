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
    /// Логика взаимодействия для EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        private static Users _users;
        private static AdminMenu _window;
        public EditUser(Users users,AdminMenu window)
        {
            if(users==null)
            {
                MessageBox.Show("Не выбран пользователь");
                Close_Click(null, null);
                return;
            }
            InitializeComponent();
            DataContext = users;
            cbOffice.ItemsSource = Manager.db.Offices.ToList();
            if (users.Roles.Title == "User") { UserRole.IsChecked = true; } else { AdminRole.IsChecked = true; }
            _users = users;
            _window = window;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            _users.FirstName = tbxFirstName.Text;
            _users.LastName = tbxLastName.Text;
            _users.OfficeID = Manager.db.Offices.FirstOrDefault(n => n.Title == cbOffice.Text).ID;
            _users.RoleID = UserRole.IsChecked.Value?2:1;
            Manager.db.SaveChanges();
            MessageBox.Show("Complete");
            _window.dgView.ItemsSource = Manager.db.Users.ToList();
        }
    }
}
