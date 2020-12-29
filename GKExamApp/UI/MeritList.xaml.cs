using GKExamApp.Data;
using GKExamApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for MeritList.xaml
    /// </summary>
    public partial class MeritList
    {
        private readonly ApplicationDbContext _db;
        private List<User> _users;

        public MeritList()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            BindUserList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new AdminDashboard();
            dashboard.Show();
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
                ShowGrid.ItemsSource = _users;
                return;
            }

            ShowGrid.ItemsSource = null;
            ShowGrid.ItemsSource = _users.Where(x => x.Name.ToLower()
            .Contains(name.ToLower()));
        }

        private void BindUserList()
        {
            _users = _db.Users.Include("Exams").ToList();

            for (var i = 0; i < _users.Count; i++)
            {
                _users[i].Index = i + 1;
            }

            ShowGrid.ItemsSource = _users;
        }
    }
}
