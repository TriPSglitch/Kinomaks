using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для BuyingATicket.xaml
    /// </summary>
    public partial class BuyingATicket : Window
    {
        Brush color;
        Films film;
        Hall hall;
        List<int> numberOfSeats = new List<int>();
        int idTimetable;

        public BuyingATicket(int idTimetable)
        {
            InitializeComponent();
            
            this.idTimetable = idTimetable;

            foreach (UIElement item in Seats.Children)
            {
                color = ((Button)item).Background;
                break;
            }

            film = Connection.db.FilmTimetable.Where(item => item.IDTimeTable == idTimetable).Select(item => item.Films).FirstOrDefault();
            Title.Content = film.Title;
            Logo.Source = ImagesManip.NewImage(film);

            Time.Content = Connection.db.Timetable.Where(item => item.ID == idTimetable).Select(item => item.Time + " " + item.Date).FirstOrDefault();

            hall = Connection.db.Timetable.Where(item => item.ID == idTimetable).Select(item => item.Hall).FirstOrDefault();
            Hall.Content = hall.ID;

            int tempNumberOfSeat = 0;

            foreach (UIElement seat in Seats.Children)
            {
                tempNumberOfSeat++;
                if (!Connection.db.Places.Where(item => item.IDHall == hall.ID).Select(item => item.Number).Contains(tempNumberOfSeat))
                {
                    ((Button)seat).Visibility = Visibility.Hidden;
                    continue;
                }

                if (Connection.db.UserTicket.Where(item => item.Timetable.ID == idTimetable).Select(item => item.IDPlace).Contains(tempNumberOfSeat))
                {
                    ((Button)seat).Background = Brushes.Red;
                }
            }

            Price.Content = string.Format("{0:f2}", film.Price);
        }

        private void ClickOnCell(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Background == Brushes.Red)
            {
                ErrorWindow errorWindow = new ErrorWindow("место уже занято");
                errorWindow.Show();
                return;
            }

            if (((Button)sender).Background == color)
            {
                ((Button)sender).Background = Brushes.Blue;

                numberOfSeats.Add((Grid.GetRow((Button)sender) * 10) + 1 + Grid.GetColumn((Button)sender) + 1);
            }    
            else if (((Button)sender).Background == Brushes.Blue)
            {
                ((Button)sender).Background = color;

                numberOfSeats.Remove((Grid.GetRow((Button)sender) * 10) + 1 + Grid.GetColumn((Button)sender) + 1);
            }

            CountOfSeats.Content = numberOfSeats.Count;
        }

        private void BuyButtonClick(object sender, RoutedEventArgs e)
        {
            if (numberOfSeats.Count == 0)
            {
                ErrorWindow errorWindow = new ErrorWindow("место не выбрано");
                errorWindow.Show();
                return;
            }

            PaymentMethod paymentMethod = new PaymentMethod(film.ID, numberOfSeats, idTimetable);
            paymentMethod.Show();
            this.Close();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}