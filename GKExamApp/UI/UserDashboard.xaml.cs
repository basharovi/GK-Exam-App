using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class UserDashboard
    {
        public UserDashboard()
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
            MessageBox.Show("Under Construction! Thanks :D");
        }

        private void ShowAllButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Under Construction! Thanks :D");
        }

        private void P_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Under Construction! Thanks :D");
        }


        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Under Construction! Thanks :D");
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
    }
}
