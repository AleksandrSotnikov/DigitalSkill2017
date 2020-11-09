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
    /// Логика взаимодействия для NoLogout.xaml
    /// </summary>
    public partial class NoLogout : Window
    {
        LoginUsers _login = null;
        public NoLogout(LoginUsers login)
        {
            InitializeComponent();
            _login = login;
            _login.Reason = "Check";
            Manager.db.SaveChanges();
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Soft.IsChecked.Value || Systems.IsChecked.Value)
            {
                _login.Reason = Warnq.Text;
                if (Soft.IsChecked.Value) _login.Cause = "Soft";
                if (Soft.IsChecked.Value) _login.Cause = "System";
                Manager.db.SaveChanges();
                this.Close();
            }
        }
    }
}
