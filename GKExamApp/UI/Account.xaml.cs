using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

            _db = new ApplicationDbContext();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new AdminDashboard();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }
       

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void SubmitButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsInputValid() == false)
                return;


            //_db.Questions.Add(GetQuestionFromUi());

            var message = _db.SaveChanges() > 0 ? "Question Added Successfully!" : "Add Failed!";

            MessageBox.Show(message);

            Clear();
        }

        private void Clear()
        {
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            EmailTextBox.Clear();
            PasswordTextBox.Clear();
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
            if (string.IsNullOrEmpty(PasswordTextBox.Password))
            {
                MessageBox.Show("Password is Empty!");
                return false;
            }

            return true;
        }
    }
}
