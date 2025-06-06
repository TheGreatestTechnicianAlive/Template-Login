﻿using System;
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

namespace Template_Login
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext db = new DataClasses1DataContext(Properties.Settings.Default.NULibraryConnectionString);

        public MainWindow()
        {
            InitializeComponent();
        }
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            if (user_txt.Text == "" || pass_txt.Text == "")
            {
                MessageBox.Show("Please enter username and password");
                return;
            }
            else
            {
                if (comparingPassword(fetchingPassword()) == 0)
                {
                    MessageBox.Show("Login Successful", "Welcome Back!", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                    var users = (from u in db.Students
                                 where u.StudentID == user_txt.Text
                                 select u).FirstOrDefault();

                    if (users.UserRole == "Admin")
                    {
                        adminHome adminHome = new adminHome();
                        adminHome.Show();
                        this.Close();
                    }
                    else if (users.UserRole == "Librarian")
                    {
                        //librarianHome librarianHome = new librarianHome();
                        //librarianHome.Show();
                        //this.Close();
                    }
                    else if (users.UserRole == "Student")
                    {
                        StudentWindow studentHomepage = new StudentWindow();
                        studentHomepage.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private string fetchingPassword()
        {
            string fetchedPassword = "";

            var users = (from u in db.Students
                         where u.StudentID == user_txt.Text
                         select u).FirstOrDefault();

            if (users == null)
            {
                return "";
            }

            fetchedPassword = users.Password;
            return fetchedPassword;
        }

        private int comparingPassword(string fetchedPassword)
        {
            if (pass_txt.Text == fetchedPassword)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }
}
