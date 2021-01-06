using GKExamApp.Data;
using GKExamApp.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for MeritList.xaml
    /// </summary>
    public partial class MeritList
    {
        private readonly ApplicationDbContext _db;
        private List<MeritViewModel> _meritList;

        public MeritList()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            BindUserList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new UserDashboard();
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
                ShowGrid.ItemsSource = _meritList;
                return;
            }

            ShowGrid.ItemsSource = null;
            ShowGrid.ItemsSource = _meritList.Where(x => x.Name.ToLower()
            .Contains(name.ToLower()));
        }

        private void BindUserList()
        {
            _meritList = new List<MeritViewModel>();

            var users = _db.Users.Include("Exams").ToList();

            foreach (var item in users)
            {
                _meritList.Add(new MeritViewModel
                {
                    Name = item.Name,
                    Username = item.Username,
                    TotalScore = item.Exams.Sum(x => x.Mark),
                    TotalExam = item.Exams.Count
                });
            }

            _meritList = _meritList.OrderByDescending(x => x.TotalScore).ToList();

            for (var i = 0; i < users.Count; i++)
            {
                _meritList[i].Position = i + 1;
            }

            ShowGrid.ItemsSource = _meritList;
        }
    }
}
