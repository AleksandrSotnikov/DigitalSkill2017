﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
            tbHello.Text = "Hi, " + Manager.db.Users.FirstOrDefault(n=>n.ID==loginUsers.UserID).FirstName + " " + Manager.db.Users
                .FirstOrDefault(n => n.ID == loginUsers.UserID).LastName + ", Welcome to AMONIC AirLine.";
            var qw = Manager.db.LoginUsers.Where(n => n.UserID == loginUsers.UserID).Select(n =>DbFunctions.DiffSeconds(n.DateTimeLogin,n.DateTimeExit )).Sum().Value;      
            var list = Manager.db.LoginUsers.Where(n => n.Cause == "System" && n.UserID == loginUsers.UserID).ToList();
            dtqqq.Text = qw.ToString();
            tbTimeSpent.Text = "Time spent on System: " + qw/3600+":"+qw%3600/60+":"+qw%60;
            tbNumber.Text = "Number of crashes: " + Manager.db.LoginUsers.Where(n=>n.Cause == "Soft" && n.UserID==loginUsers.UserID).Count();
            dgView.ItemsSource = Manager.db.LoginUsers.Where(n => n.UserID == loginUsers.UserID)
                .Select(q => new { q.DateTimeLogin, q.DateTimeExit, qwe = DbFunctions.DiffSeconds(q.DateTimeLogin, q.DateTimeExit)/3600 + ":" + DbFunctions.DiffSeconds(q.DateTimeLogin, q.DateTimeExit) % 3600/60 + ":" + DbFunctions.DiffSeconds(q.DateTimeLogin, q.DateTimeExit) % 60 ,q.Reason, q.Cause })
                .ToList();
            Binding binding = new Binding();
              int[] s ={ 1,2,3,4,5,6,7};
            binding.Source = s;

        }

        private void MenuItemExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
            _loginUsers.DateTimeExit = DateTime.Now;
            _loginUsers.Cause = "Good";
            Manager.db.SaveChanges();
            Close();
        }

        private void dgView_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Background = e.Row.Item.ToString().Contains("Soft")|| e.Row.Item.ToString().Contains("System") ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
            //this.Title = e.Row.Item.ToString();
        }
    }
}
