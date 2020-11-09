using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Data.Entity;

namespace DigitalSkills2017
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        static int countauth = 0;
        static int t = 10;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Login()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
            {
                Application.Current.Shutdown();
            }

        private void timerTick(object sender, EventArgs e)
        {
            t--;
            tbTime.Text = t.ToString();
            if (t <= 0)
            {
                timer.Stop();
                t = 10;
                countauth = 0;
                btnLogin.IsEnabled = true;
                tbTime.Visibility = Visibility.Hidden;
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
            {

                try
                {
                    int Role;
                     var Roles = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text && n.Password == tbxPassword.Text);
                if (Roles != null) Role = int.Parse(Roles.RoleID.ToString());
                else throw new Exception();
                    bool? Active = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text && n.Password == tbxPassword.Text).Active;
                    if (!Active.Value)
                    {
                        MessageBox.Show("Пользователь деактивирован");
                        return;
                    }
                    LoginUsers loginUsers = new LoginUsers
                    {
                        UserID = Manager.db.Users.FirstOrDefault(n => n.Email == tbxLogin.Text).ID,
                        DateTimeLogin = DateTime.Now,
                        DateTimeExit = DateTime.Now,
                        Cause = "Soft",
                        Reason = "Good"
                    };
                    switch (Role)
                    {
                        case 1:
                            new AdminMenu(loginUsers).Show();
                            new AuthCheck().Show();
                            break;
                        case 2:
                            new UserMenu(loginUsers).Show();
                     var z=   Manager.db.LoginUsers.Where(n => n.UserID == loginUsers.UserID).ToList();
                        if(z.Last().Cause.Contains("Soft")|| z.Last().Cause.Contains("System")) new NoLogout(z.Last()).Show();
                        break;
                    }
                    Manager.db.LoginUsers.Add(loginUsers);
                    Manager.db.SaveChanges();
                    Application.Current.MainWindow.Visibility = Visibility.Hidden;
               }
               catch
               {
                    MessageBox.Show("Некорректные данные");
                    countauth++;
                    
                    if (countauth == 3)
                    {     
                        timer.Start();
                    btnLogin.IsEnabled = false;
                    tbTime.Visibility = Visibility.Visible;
                }
            }
         }
     }
}

