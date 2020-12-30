using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using GKExamApp.Data;
using GKExamApp.Models;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for AddQuestion.xaml
    /// </summary>
    public partial class Exam
    {
        private readonly string _assetDirectory = Directory.GetCurrentDirectory() + @"\Assets";
        private readonly ApplicationDbContext _db;

        public Exam()
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

        private void StartCountdown(FrameworkElement target, int timerLength)
        {
            var countdownAnimation = new StringAnimationUsingKeyFrames();
            for (var i = timerLength; i > 0; i--)
            {
                var keyTime = TimeSpan.FromSeconds(timerLength - i);
                var frame = new DiscreteStringKeyFrame(i.ToString(), KeyTime.FromTimeSpan(keyTime));
                countdownAnimation.KeyFrames.Add(frame);
            }
            //countdownAnimation.KeyFrames.Add(new DiscreteStringKeyFrame(" ", KeyTime.FromTimeSpan(TimeSpan.FromSeconds(6))));
            Storyboard.SetTargetName(countdownAnimation, target.Name);
            Storyboard.SetTargetProperty(countdownAnimation, new PropertyPath(TextBlock.TextProperty));

            var countdownStoryboard = new Storyboard();
            countdownStoryboard.Children.Add(countdownAnimation);
            countdownStoryboard.Completed += CountdownTimer_Completed;
            countdownStoryboard.Begin(this);
        }

        private void CountdownTimer_Completed(object sender, EventArgs e)
        {
            MessageBox.Show("Time's up!");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Loaded += (o, args) => StartCountdown(CountdownDisplay, 120);
        }
    }
}
