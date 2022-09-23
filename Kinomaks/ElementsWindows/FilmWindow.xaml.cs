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
            Logo.Source = ImagesManip.NewImage(film);
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            FilmsListWindow filmsListWindow = new FilmsListWindow();
            filmsListWindow.Show();
            this.Close();
        }
    }
}