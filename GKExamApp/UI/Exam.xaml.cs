using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for Exam.xaml
    /// </summary>
    public partial class Exam
    {
        public Exam()
        {
            InitializeComponent();
            MouseDoubleClick += (o, args) => StartCountdown(CountdownDisplay);
        }

        private void StartCountdown(FrameworkElement target)
        {
            var countdownAnimation = new StringAnimationUsingKeyFrames();
            var timerTime = 35;
            for (var i = timerTime; i > 0; i--)
            {
                var keyTime = TimeSpan.FromSeconds(timerTime - i);
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

        private void MeritListButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Under Construction! Thanks :D");
        }

        private void P_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Under Construction! Thanks :D");
        }


        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new Account();
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
