using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.IO;
using System.Windows;
using GKExamApp.Data;
using GKExamApp.Models;
using MessageBox = System.Windows.MessageBox;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for UpdateQuestion.xaml
    /// </summary>
    public partial class UpdateQuestion
    {
        private readonly string _assetDirectory = Directory.GetCurrentDirectory() + @"\Assets";
        private readonly ApplicationDbContext _db;
        private readonly int _id;

        public UpdateQuestion(int id)
        {
            InitializeComponent();

            _id = id;
            _db = new ApplicationDbContext();
            AnswerComboBox.ItemsSource = BindAnswerCombobox();
            FillGrid();
        }

        private void FillGrid()
        {
            var question = _db.Questions.Find(_id);

            if (question == null) 
                return;

            TextBoxOptionA.Text = question.OptionA;
            TextBoxOptionB.Text = question.OptionB;
            TextBoxOptionC.Text = question.OptionC;
            TextBoxOptionD.Text = question.OptionD;
            TextBoxQuestion.Text = question.QuestionText;
            AnswerComboBox.Text = question.RightAnswer;
            PointTextBox.Text = question.Point.ToString();
            TimeTextBox.Text = question.TimeDuration.ToString();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new ShowAllQuestions();
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

            _db.Questions.AddOrUpdate(GetQuestionFromUi());

            var message = _db.SaveChanges() > 0 ? "Question Updated Successfully!" : "Update Failed!";

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
            PointTextBox.Clear();
            TimeTextBox.Clear();
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

            if (string.IsNullOrEmpty(PointTextBox.Text))
            {
                MessageBox.Show("Point TextBox is Empty!");
                return false;
            }

            if (decimal.TryParse(PointTextBox.Text, out _) == false)
            {
                MessageBox.Show("Input Point is not valid fractional number");
                return false;
            }

            if (string.IsNullOrEmpty(TimeTextBox.Text))
            {
                MessageBox.Show("Time TextBox is Empty!");
                return false;
            }

            if (int.TryParse(TimeTextBox.Text, out _) == false)
            {
                MessageBox.Show("Input Time is not valid integer number");
                return false;
            }

            return true;
        }

        private Question GetQuestionFromUi()
        {
            var question = new Question()
            {
                Id = _id,
                QuestionText = TextBoxQuestion.Text,
                OptionA = TextBoxOptionA.Text,
                OptionB = TextBoxOptionB.Text,
                OptionC = TextBoxOptionC.Text,
                OptionD = TextBoxOptionD.Text,
                RightAnswer = AnswerComboBox.Text,
                Point = Convert.ToDecimal(PointTextBox.Text),
                TimeDuration = Convert.ToInt32(TimeTextBox.Text)
            };

            return question;
        }
    }
}
