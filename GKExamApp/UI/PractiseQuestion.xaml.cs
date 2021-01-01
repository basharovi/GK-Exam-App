using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using GKExamApp.Data;
using GKExamApp.Helper;
using GKExamApp.Models;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for PractiseQuestion.xaml
    /// </summary>
    public partial class PractiseQuestion
    {
        private readonly ApplicationDbContext _db;
        private readonly List<Question> _questionList;

        public PractiseQuestion()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            _questionList = _db.Questions.ToList();
            AnswerComboBox.ItemsSource = BindAnswerCombobox();

            BeReadyForNextQuestion();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Utilities.BackToDashboard();
        }

        private static IEnumerable<string> BindAnswerCombobox()
        {
            var optionList = new List<string>
            {
                "Option-A", "Option-B", "Option-C", "Option-D"
            };

            return optionList;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void BindQuestionsToUi(Question question)
        {
            TextBoxOptionA.Text = question.OptionA;
            TextBoxOptionB.Text = question.OptionB;
            TextBoxOptionC.Text = question.OptionC;
            TextBoxOptionD.Text = question.OptionD;
            TextBoxQuestion.Text = question.QuestionText;
            AnswerComboBox.Text = question.RightAnswer;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            BeReadyForNextQuestion();
        }

        private void BeReadyForNextQuestion()
        {
            BindQuestionsToUi(_questionList.NextRandom());
        }
    }
}
