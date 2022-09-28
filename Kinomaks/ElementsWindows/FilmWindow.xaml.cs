using System.Linq;
using System.Windows;
using Kinomaks.ListWindows;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для FilmWindow.xaml
    /// </summary>
    public partial class FilmWindow : Window
    {
        Films film;
        public FilmWindow(int id)
        {
            InitializeComponent();
            film = Connection.db.Films.Where(item => item.ID == id).FirstOrDefault();
            Title.Content = film.Title;
            Price.Content = string.Format("{0:f2}", film.Price);
            Description.Text = film.Description;
            Logo.Source = ImagesManip.NewImage(film);
        }

        private void GoToTimetableClick(object sender, RoutedEventArgs e)
        {
            TimetableWindow timetableWindow = new TimetableWindow(film.ID);
            timetableWindow.Show();
            this.Close();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            FilmsListWindow filmsListWindow = new FilmsListWindow();
            filmsListWindow.Show();
            this.Close();
        }
    }
}