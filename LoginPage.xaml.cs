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
             
            //try
            //{
                int Role = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text && n.Password == tbxPassword.Text).RoleID;
                switch (Role)
                {
                    case 1:
                        new AdminMenu().Show();
                        break;
                    case 2:
                        new UserMenu().Show();
                        break;
                }
                Application.Current.MainWindow.Visibility = Visibility.Hidden;
            //}
            //catch
            //{
            //    MessageBox.Show("Некорректные данные");
            //}
        }
    }
}
