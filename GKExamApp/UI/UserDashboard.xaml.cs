﻿using System.ComponentModel;
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

        private void MeritListButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new MeritList();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }

        private void ExamButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new Exam();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }


        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new Account();
            test.Closed += (s, args) => Close();
            test.Show();
            Hide();
        }
        private void QuestionBankButton_Click(object sender, RoutedEventArgs e)
        {
            var test = new PractiseQuestion();
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
