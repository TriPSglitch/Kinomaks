using System.Windows;
using Kinomaks.ListWindows;
using Kinomaks.AddWindows;
using Kinomaks.ElementsWindows;

namespace Kinomaks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            if (User.Role != 1)
            {
                AddItem.Visibility = Visibility.Hidden;
            }
        }
        #region Переход на другие окна

        #region Окна добавления
        private void AddFilmClick(object sender, RoutedEventArgs e)
        {
            AddFilmWindow addFilmWindow = new AddFilmWindow();
            addFilmWindow.Show();
            this.Close();
        }

        private void AddTimetableClick(object sender, RoutedEventArgs e)
        {
            AddTimetableWindow addTimetableWindow = new AddTimetableWindow();
            addTimetableWindow.Show();
            this.Close();
        }

        private void AddHallClick(object sender, RoutedEventArgs e)
        {
            AddHallWindow addHallWindow = new AddHallWindow();
            addHallWindow.Show();
            this.Close();
        }

        private void AddCinemaClick(object sender, RoutedEventArgs e)
        {
            AddCinemaWindwow addCinemaWindwow = new AddCinemaWindwow();
            addCinemaWindwow.Show();
            this.Close();
        }
        #endregion

        #region Окна списков
        private void FilmsListClick(object sender, RoutedEventArgs e)
        {
            FilmsListWindow FilmsListWindow = new FilmsListWindow();
            FilmsListWindow.Show();
            this.Close();
        }

        private void TimetableListClick(object sender, RoutedEventArgs e)
        {
            TimetableWindow timetableWindow = new TimetableWindow();
            timetableWindow.Show();
            this.Close();
        }

        private void ReturnClick(object sender, RoutedEventArgs e)
        {
            ReturnWindow returnWindow = new ReturnWindow();
            returnWindow.Show();
            this.Close();
        }

        private void CinemaListClick(object sender, RoutedEventArgs e)
        {
            CinemaListWindow cinemaListWindow = new CinemaListWindow();
            cinemaListWindow.Show();
            this.Close();
        }
        #endregion

        #endregion
    }
}