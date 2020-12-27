using System;
using System.ComponentModel;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using GKExamApp.Data;
using GKExamApp.Models;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class Account
    {
        private readonly string _assetDirectory = Directory.GetCurrentDirectory() + @"\Assets";
        private readonly ApplicationDbContext _db;

        public Account()
        {
            InitializeComponent();

            BindUi();
            _db = new ApplicationDbContext();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            GoToDashboard();
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void SubmitButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsInputValid() == false)
                    return;

                var aUser = GetUserFromUi();

                _db.Entry(aUser).State = EntityState.Modified;

                var message = _db.SaveChanges() > 0 ? "Data Update Successful!" : "Data Update Failed!";

                MessageBox.Show(message);

                Utilities.SetUserModel(aUser);

                GoToDashboard();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private User GetUserFromUi()
        {
            var user = _db.Users.Find(Utilities.UserModel.Id);

            user.Name = FirstNameTextBox.Text + " " + LastNameTextBox.Text;
            user.Email = EmailTextBox.Text;
            user.Password = PasswordTextBox.Password;

            return user;
        }

        private void BindUi()
        {
            var name = Utilities.UserModel.Name.Split(' ');

            FirstNameTextBox.Text = name?[0];
            LastNameTextBox.Text = name?[1];
            EmailTextBox.Text = Utilities.UserModel.Email;
            PasswordTextBox.Password = Utilities.UserModel.Password;
        }

        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(FirstNameTextBox.Text))
            {
                MessageBox.Show("First Name is Empty!");
                return false;
            }
            if (string.IsNullOrEmpty(LastNameTextBox.Text))
            {
                MessageBox.Show("Last Name is Empty!");
                return false;
            }
            if (string.IsNullOrEmpty(EmailTextBox.Text))
            {
                MessageBox.Show("Email is Empty!");
                return false;
            }
            if (IsValidEmailAddress(EmailTextBox.Text) == false)
            {
                MessageBox.Show("Please, Enter a valid Email Address!");
                return false;
            }
            if (string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Password is Empty!");
                return false;
            }

            return true;
        }

        private void GoToDashboard()
        {
            if (Utilities.UserModel.Role.Equals("Admin"))
            {
                var admin = new AdminDashboard();
                admin.Show();
                Hide();
            }
            else
            {
                var user = new UserDashboard();
                user.Show();
                Hide();
            }
        }

        private static bool IsValidEmailAddress(string email)
        {
            var regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");

            return regex.IsMatch(email);
        }
    }
}
