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
    /// Логика взаимодействия для AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            cbOffice.ItemsSource = Manager.db.Offices.Select(n => n.Title).ToList();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if(tbEmail.Text!=null && tbEmail.Text.Contains('@') && tbEmail.Text.IndexOf('@') == tbEmail.Text.LastIndexOf('@') && tbEmail.Text.Contains('.')&&(tbEmail.Text.Length>5))
            {
                if(tbFirstName.Text.Length >=1 && tbLastName.Text.Length >= 1)
                {
                    if(tbBirthday.Text.Length==8 && tbBirthday.Text.IndexOf("/")==2 && tbBirthday.Text.LastIndexOf("/") == 5)
                    {
                        if (cbOffice.SelectedIndex > 0)
                        {
                            if (tbPassword.Text.Length > 1)
                            {
                                Users users = new Users();
                                users.RoleID = 2; 
                                users.FirstName =tbFirstName.Text;
                                users.LastName =tbLastName.Text;
                                users.Email =tbEmail.Text;
                                users.Password = tbPassword.Text;
                                users.OfficeID = Manager.db.Offices.Where(n => n.Title == cbOffice.Text).Select(j => j.ID).ToArray()[0];
                                users.Birthdate = new DateTime();
                                users.Active = true;
                                Manager.db.Users.Add(users);
                                try
                                {
                                    Manager.db.SaveChanges();
                                    MessageBox.Show("Save");
                                }
                                catch
                                {
                                    MessageBox.Show("ОШИБКА при сохранении в БД, возмложно email зарегистрирован");
                                }
                                
                            }
                        }
                    }
                }
            }
        }
    }
}
