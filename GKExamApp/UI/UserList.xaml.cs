using GKExamApp.Data;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for UserList.xaml
    /// </summary>
    public partial class UserList
    {
        private readonly ApplicationDbContext _db;
        private bool _isGridRowSelected;

        public UserList()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            ShowGrid.ItemsSource = _db.Users.ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new AdminDashboard();
            dashboard.Show();
            Hide();
        }


        private void ShowGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _isGridRowSelected = true;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGridRowSelected)
            {
                MessageBox.Show("No row has been selected!");
                return;
            }

            Hide();

        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void SearchByName_KeyUp(object sender, KeyEventArgs e)
        {
            var name = SearchByNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowGrid.ItemsSource = _db.Users.ToList();
                return;
            }

            ShowGrid.ItemsSource = null;
            ShowGrid.ItemsSource = _db.Users.Where(x => x.Name.ToLower()
            .Contains(name.ToLower())).ToList();
        }
    }
}
