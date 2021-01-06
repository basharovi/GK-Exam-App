using System;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GKExamApp.Data;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {

        private readonly ApplicationDbContext _db;
        public Login()
        {
            InitializeComponent();
            _db = new ApplicationDbContext();
        }


        private async void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await LogIn();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private async void Window_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key != Key.Enter) return;
                e.Handled = true;

                await LogIn();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }


        private async Task LogIn()
        {
            try
            {
                if (IsInputValid() == false)
                    return;

                var theUser = await _db.Users.FirstOrDefaultAsync(m => m.Username.Equals(UserNameTextBox.Text)
                                                                       && m.Password.Equals(PasswordTextBox.Password));

                if (theUser == null)
                {
                    MessageBox.Show("Username or Password is incorrect!");
                    return;
                }

                Utilities.SetUserModel(theUser);

                Hide();
                Utilities.BackToDashboard();
            }

            catch (SqlException)
            {
                MessageBox.Show("Please, connect to internet!");
                return;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return;
        }

        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(UserNameTextBox.Text))
            {
                MessageBox.Show("Please, Enter Username!");
                return false;
            }

            if (string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Please, Enter Password!");
                return false;
            }

            return true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void ForgetPasswordLabel_OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //MessageBox.Show("Clicked!");
        }

        private void SignUpButton_Click(object sender, RoutedEventArgs e)
        {
            var signUp = new SignUp();
            signUp.Show();
            Hide();
        }
    }
}
