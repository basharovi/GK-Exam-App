using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using GKExamApp.Data;
using GKExamApp.Models;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp
    {
        private readonly ApplicationDbContext _db;
        public SignUp()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
        }


        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = new Login();
                login.Show();
                Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) return;
                e.Handled = true;

                DoSignUp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void DoSignUp()
        {
            try
            {
                if (IsInputValid() == false)
                    return;

                var aUser = GetUserFromUi();
                aUser.Role = "User";

                _db.Users.Add(aUser);

                var message = _db.SaveChanges() > 0 ? "Registration Successful!" : "Registration Failed!";

                MessageBox.Show(message);

                var login = new Login();
                login.Show();
                Hide();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new Login();
            login.Show();
            Hide();
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DoSignUp();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("Please, Enter First Name!");
                return false;
            }
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Please, Enter Last Name!");
                return false;
            }
            if (string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                MessageBox.Show("Please, Enter Username!");
                return false;
            }
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Please, Enter Email!");
                return false;
            }

            if (IsValidEmailAddress(EmailTextBox.Text) == false)
            {
                MessageBox.Show("Please, Enter a valid Email Address!");
                return false;
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Please, Enter Password!");
                return false;
            }

            if (PasswordTextBox.Password.Length < 6)
            {
                MessageBox.Show("Minimum 6 Digit of Password is required!");
                return false;
            }

            if (string.IsNullOrEmpty(ConfirmPasswordTextBox.Password))
            {
                MessageBox.Show("Please, Enter the Confirm Password!");
                return false;
            }

            if (PasswordTextBox.Password.Equals(ConfirmPasswordTextBox.Password) == false)
            {
                MessageBox.Show("Password & Confirm Password doesn't matched!");
                return false;
            }

            if (_db.Users.Any(u => u.Username.Equals(UserNameTextBox.Text)))
            {
                MessageBox.Show("This Username is already exist!");
                return false;
            }

            return true;
        }

        private static bool IsValidEmailAddress(string email)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            return regex.IsMatch(email);
        }

        private User GetUserFromUi()
        {
            var aUser = new User()
            {
                Name = FirstNameTextBox.Text + " "  + LastNameTextBox.Text,
                Username = UserNameTextBox.Text,
                Email = EmailTextBox.Text,
                Password = PasswordTextBox.Password
            };

            return aUser;
        }
    }
}
