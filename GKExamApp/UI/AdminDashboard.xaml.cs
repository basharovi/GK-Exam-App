using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class AdminDashboard
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void Logout_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var test = new Login();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }

        private void CSVInputButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new Accounts();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new ShowAllQuestions();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }

        private void UserListButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new UserList();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new AddQuestion();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }
}
