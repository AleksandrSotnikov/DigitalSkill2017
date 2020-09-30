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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DigitalSkills2017
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        Window window;
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
             
            try
            {
                int Role = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text && n.Password == tbxPassword.Text).RoleID;
                bool? Active = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text && n.Password == tbxPassword.Text).Active;
                if (!Active.Value)
                {
                    MessageBox.Show("Пользователь деактивирован");
                    return;
                }
                switch (Role)
                {
                    case 1:
                        new AdminMenu().Show();
                        break;
                    case 2:
                        new UserMenu().Show();
                        break;
                }
                LoginUsers loginUsers = new LoginUsers
                {
                    UserID = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text).ID,
                    DateTimeLogin = DateTime.Now,
                    DateTimeExit = DateTime.Today,
                    Cause = "System"
                };
                Manager.db.LoginUsers.Add(loginUsers);
                Manager.db.SaveChanges();
                Application.Current.MainWindow.Visibility = Visibility.Hidden;
            }
            catch
            {
                MessageBox.Show("Некорректные данные");
            }
        }
    }
}
