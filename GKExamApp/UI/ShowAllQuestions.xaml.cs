using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using GKExamApp.Data;
using GKExamApp.Models;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for ShowAllQuestions.xaml
    /// </summary>
    public partial class ShowAllQuestions
    {
        private bool _isGridRowSelected;
        private readonly ApplicationDbContext _db;
        private Question _question;

        public ShowAllQuestions()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            _question = new Question();

            ShowGrid.ItemsSource = GetQuestionList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new AdminDashboard();
            dashboard.Show();
            Hide();
        }

        private IEnumerable<Question> GetQuestionList()
        {
            var questions = _db.Questions.ToList();

            for (var i = 0; i < questions.Count; i++)
            {
                questions[i].Index = i + 1;
            }

            return questions;
        }

        private void ShowGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _isGridRowSelected = true;
            _question = ShowGrid.SelectedItem as Question;
        }

       
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGridRowSelected)
            {
                MessageBox.Show("No row has been selected!");
                return;
            }

            var update = new UpdateQuestion(_question.Id);
            update.Show();
            Hide();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void Search_KeyUp(object sender, KeyEventArgs e)
        {
            ShowGrid.ItemsSource = GetQuestionList()
                    .Where(q=> q.QuestionText
                    .Contains(SearchTextBox.Text))
                    .ToList();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (!_isGridRowSelected)
            {
                MessageBox.Show("No row has been selected!");
                return;
            }

            var question = _db.Questions.Find(_question.Id);
            if (question == null)
            {
                MessageBox.Show("No row has been selected!");
                return;
            }

            _db.Questions.Remove(question);

            var message = _db.SaveChanges() > 0 ? "Question Deleted Successfully!" : "Delete Failed!";

            MessageBox.Show(message);

            ShowGrid.ItemsSource = null;
            ShowGrid.ItemsSource = _db.Questions.ToList();
        }
    }
    
}
