using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using GKExamApp.Data;
using GKExamApp.Models;
using MessageBox = System.Windows.MessageBox;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class AddQuestion
    {
        private readonly string _assetDirectory = Directory.GetCurrentDirectory() + @"\Assets";
        private readonly ApplicationDbContext _db;

        public AddQuestion()
        {
            InitializeComponent();

            _db = new ApplicationDbContext();
            AnswerComboBox.ItemsSource = BindAnswerCombobox();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new AdminDashboard();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
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

        private void SubmitButton_OnClickButton_Click(object sender, RoutedEventArgs e)
        {
            if(IsInputValid() == false)
                return;


            _db.Questions.Add(GetQuestionFromUi());

            var message = _db.SaveChanges() > 0 ? "Question Added Successfully!" : "Add Failed!";

            MessageBox.Show(message);

            Clear();
        }

        private void Clear()
        {
            TextBoxOptionA.Clear();
            TextBoxOptionB.Clear();
            TextBoxOptionC.Clear();
            TextBoxOptionD.Clear();
            TextBoxQuestion.Clear();

        }

        private bool IsInputValid()
        {
            if (string.IsNullOrEmpty(TextBoxQuestion.Text))
            {
                MessageBox.Show("Question TextBox is Empty!");
                return false;
            }
            if (string.IsNullOrEmpty(TextBoxOptionA.Text))
            {
                MessageBox.Show("Option A is Empty!");
                return false;
            }
            if (string.IsNullOrEmpty(TextBoxOptionB.Text))
            {
                MessageBox.Show("Option B is Empty!");
                return false;
            }
            if (string.IsNullOrEmpty(TextBoxOptionC.Text))
            {
                MessageBox.Show("Option C is Empty!");
                return false;
            }

            if (string.IsNullOrEmpty(TextBoxOptionD.Text))
            {
                MessageBox.Show("Option D is Empty!");
                return false;
            }

            return true;
        }

        private Question GetQuestionFromUi()
        {
            var question = new Question()
            {
                QuestionText = TextBoxQuestion.Text,
                OptionA = TextBoxOptionA.Text,
                OptionB = TextBoxOptionB.Text,
                OptionC = TextBoxOptionC.Text,
                OptionD = TextBoxOptionD.Text,
                RightAnswer = AnswerComboBox.Text
            };

            return question;
        }
    }
}
