using System.ComponentModel;
using System.Windows;

namespace GKExamApp.UI
{
    /// <summary>
    /// Interaction logic for ShowAll.xaml
    /// </summary>
    public partial class ShowAll
    {
        private bool _isGridRowSelected;
        //private readonly string _assetDirectory = Directory.GetCurrentDirectory() + @"\Assets\FireService";

        public ShowAll()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            //var dashboard = new Dashboard();
            //dashboard.Show();
            //Hide();
        }


        private void ShowGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _isGridRowSelected = true;
        }

        private void SearchByName_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            SearchByPnTextBox.Clear();
            SearchByRankTextBox.Clear();
        }

        private void SearchByPn_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            SearchByNameTextBox.Clear();
            SearchByRankTextBox.Clear();
        }

        private void SearchByRank_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {

            SearchByNameTextBox.Clear();
            SearchByPnTextBox.Clear();
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
    }
    
}
