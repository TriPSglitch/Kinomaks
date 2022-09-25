using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Kinomaks.ElementsWindows;

namespace Kinomaks.ListWindows
{
    /// <summary>
    /// Логика взаимодействия для FilmsListWindow.xaml
    /// </summary>
    public partial class FilmsListWindow : Window
    {
        public FilmsListWindow()
        {
            InitializeComponent();
            FilmsList.ItemsSource = Connection.db.Films.ToList();
        }
        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
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
                FilmsList.ItemsSource = Connection.db.Films.Where(item => (item.Title + " " + item.Descripton + " " + item.Price).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                FilmsList.ItemsSource = Connection.db.Films.ToList();
            }
            #endregion
        }

        private void FilmsListMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((Films)FilmsList.SelectedItem).ID;
            FilmWindow filmWindow = new FilmWindow(id);
            filmWindow.Show();
            this.Hide();
        }
    }
}