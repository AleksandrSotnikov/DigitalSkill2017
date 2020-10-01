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
            var list = Manager.db.LoginUsers.Where(n => n.Cause == "System" && n.UserID == loginUsers.UserID).ToList();
            DateTime dt = new DateTime();
            for(int i = 0; i < list.Count; i++)
            {
               dt+= list[i].DateTimeExit - list[i].DateTimeLogin;
            }
            tbTimeSpent.Text = "Time spent on System: " + dt.Hour +":"+ dt.Minute+":"+dt.Second;
            tbNumber.Text = "Number of crashes: " + Manager.db.LoginUsers.Where(n=>n.Cause == "Soft" && n.UserID==loginUsers.UserID).Count();
            dgView.ItemsSource = Manager.db.LoginUsers.Where(n => n.UserID == loginUsers.UserID).ToList();
            Binding binding = new Binding();
              int[] s ={ 1,2,3,4,5,6,7};
            binding.Source = s;

        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            _loginUsers.DateTimeExit = DateTime.Now;
            _loginUsers.Cause = "System";
            Manager.db.SaveChanges();
            Close();
        }

        private void dgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            if (((LoginUsers)e.Row.Item).Cause == "Soft") e.Row.Background = new SolidColorBrush(Colors.Green);
            if (((LoginUsers)e.Row.Item).Cause == "System") e.Row.Background = new SolidColorBrush(Colors.Red);
        }
    }
}
