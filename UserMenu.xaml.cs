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
    /// Логика взаимодействия для UserMenu.xaml
    /// </summary>
    public partial class UserMenu : Window
    {
        private static LoginUsers _loginUsers;
        public UserMenu(LoginUsers loginUsers)
        {
            InitializeComponent();
            _loginUsers = loginUsers;
            tbHello.Text = "Hi, " + Manager.db.Users.FirstOrDefault(n=>n.ID==loginUsers.UserID).FirstName + " " + Manager.db.Users.FirstOrDefault(n => n.ID == loginUsers.UserID).LastName + ", Welcome to AMONIC AirLine.";
            tbTimeSpent.Text = "Time spent on System: ";
            tbNumber.Text = "Number of crashes: ";
        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            _loginUsers.DateTimeExit = DateTime.Now;
            _loginUsers.Cause = "System";
            Manager.db.LoginUsers.Add(_loginUsers);
            Manager.db.SaveChanges();
            Close();
        }
    }
}
