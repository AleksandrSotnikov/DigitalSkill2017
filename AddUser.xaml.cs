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
            if (tbEmail.Text != null && tbEmail.Text.Contains('@') && tbEmail.Text.IndexOf('@') == tbEmail.Text.LastIndexOf('@') && tbEmail.Text.Contains('.') && (tbEmail.Text.Length > 5))
            {
                int i = Manager.db.Users.Where(n => n.Email == tbEmail.Text).Count();
                if (i == 0)
                {
                    if (tbFirstName.Text.Length >= 1 && tbLastName.Text.Length >= 1)
                    {
                        if (tbBirthday.Text.Length == 8 && tbBirthday.Text.IndexOf("/") == 2 && tbBirthday.Text.LastIndexOf("/") == 5)
                        {
                            if (cbOffice.SelectedIndex >= 0)
                            {
                                if (tbPassword.Text.Length > 1)
                                {
                                    Users users = new Users
                                    {
                                        RoleID = 2,
                                        FirstName = tbFirstName.Text,
                                        LastName = tbLastName.Text,
                                        Email = tbEmail.Text,
                                        Password = tbPassword.Text,
                                        OfficeID = Manager.db.Offices.Where(n => n.Title == cbOffice.Text).Select(j => j.ID).ToArray()[0],
                                        Birthdate = Convert.ToDateTime(tbBirthday.Text),
                                        Active = true
                                    };
                                    Manager.db.Users.Add(users);
                                    try
                                    {
                                        Manager.db.SaveChanges();
                                    }
                                    catch (System.Data.Entity.Infrastructure.DbUpdateException)
                                    {
                                        MessageBox.Show("Ошибка добавления");

                                    }
                                    MessageBox.Show("Save");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите дату Рождения в формате dd/mm/yy");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Email Уже зарегистрирован");
                }
            }
        }
    }
}
