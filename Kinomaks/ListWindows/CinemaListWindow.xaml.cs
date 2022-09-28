using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kinomaks.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для CinemaListWindow.xaml
    /// </summary>
    public partial class CinemaListWindow : Window
    {
        public CinemaListWindow()
        {
            InitializeComponent();
            CinemaList.ItemsSource = Connection.db.Cinema.ToList();
        }

        private void SearchPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            #region Поиск
            if (Search.Text == "Поиск")
                Search.Text = "";
        }

        private void SearchLostFocus(object sender, RoutedEventArgs e)
        {
            if (Search.Text == "")
                Search.Text = "Поиск";
        }

        private void SearchTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Search.Text != "" && Search.Text != "Поиск")
            {
                CinemaList.ItemsSource = Connection.db.Cinema.Where(item => (item.Name + " " + item.City + " " + item.Street + " " + item.Building).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                CinemaList.ItemsSource = Connection.db.Cinema.ToList();
            }
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
    }
}