using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для TimetableWindow.xaml
    /// </summary>
    public partial class TimetableWindow : Window
    {
        public TimetableWindow()
        {
            InitializeComponent();
            FilmsList.ItemsSource = Connection.db.FilmTimetable.ToList();
        }

        public TimetableWindow(int idFilm)
        {
            InitializeComponent();
            FilmsList.ItemsSource = Connection.db.FilmTimetable.Where(item => item.IDFilm == idFilm).ToList();
            Search.Visibility = Visibility.Hidden;
        }

        private void FilmsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int idTimetable = ((FilmTimetable)sender).IDTimeTable;
            BuyingATicket buyingATicket = new BuyingATicket(idTimetable);
            buyingATicket.Show();
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
                FilmsList.ItemsSource = Connection.db.FilmTimetable.Where(item => (item.Films.Title + " " + item.Films.Price + " " 
                                                                                    + item.Timetable.Date + " " + item.Timetable.Time).Contains(Search.Text)).ToList();
            }
            else if (Search.Text == "" || Search.Text == "Поиск")
            {
                FilmsList.ItemsSource = Connection.db.FilmTimetable.ToList();
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