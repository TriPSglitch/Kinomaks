using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для BuyingATicket.xaml
    /// </summary>
    public partial class BuyingATicket : Window
    {
        Brush color;

        public BuyingATicket(int idTimetable)
        {
            InitializeComponent();

            foreach (UIElement item in Seats.Children)
            {
                color = ((Button)item).Background;
                break;
            }

            Films film = Connection.db.FilmTimetable.Where(item => item.IDTimeTable == idTimetable).Select(item => item.Films).FirstOrDefault();
            Title.Content = film.Title;
            Logo.Source = ImagesManip.NewImage(film);

            Time.Content = Connection.db.Timetable.Where(item => item.ID == idTimetable).Select(item => item.Time).FirstOrDefault();

            Hall hall = Connection.db.HallTimetable.Where(item => item.IDTimetable == idTimetable).Select(item => item.Hall).FirstOrDefault();
            Hall.Content = hall.Number;

            int tempNumberOfSeat = 0;
            foreach (UIElement seat in Seats.Children)
            {
                tempNumberOfSeat++;
                if (!Connection.db.Places.Where(item => item.IDHall == hall.ID).Select(item => item.Number).Contains(tempNumberOfSeat))
                {
                    ((Button)seat).Visibility = Visibility.Hidden;
                    continue;
                }

                if (Connection.db.HallTimetable.Where(item => item.IDTimetable == idTimetable).Select(item => item.IDPlace).Contains(tempNumberOfSeat))
                {
                    ((Button)seat).Background = Brushes.Red;
                }
            }
        }

        private void ClickOnCell(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Background == color)
                ((Button)sender).Background = Brushes.Red;
            else if (((Button)sender).Background == Brushes.Red)
                ((Button)sender).Background = color;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BuyButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}