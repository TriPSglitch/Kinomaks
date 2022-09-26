using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Kinomaks.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddHallWindow.xaml
    /// </summary>
    public partial class AddHallWindow : Window
    {
        Brush color;

        public AddHallWindow()
        {
            InitializeComponent();

            foreach (UIElement item in Seats.Children)
            {
                color = ((Button)item).Background;
                break;
            }
        }

        private void ClickOnCell(object sender, MouseButtonEventArgs e)
        {
            if (((Button)sender).Background == color)
                ((Button)sender).Background = Brushes.Red;
            else if (((Button)sender).Background == Brushes.Red)
                ((Button)sender).Background = color;
        }

        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление зала
            if (Number.Text == "")
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return;
            }

            int countOfSeats = 0;
            foreach (UIElement item in Seats.Children)
            {
                if (((Button)item).Background == Brushes.Red)
                    continue;

                countOfSeats++;
            }

            if (countOfSeats == 0)
            {
                ErrorWindow errorWindow = new ErrorWindow("пустой зал");
                errorWindow.Show();
                return;
            }

            int tempResult;
            if (!int.TryParse(Number.Text, out tempResult))
            {
                ErrorWindow errorWindow = new ErrorWindow("номер указан неверно");
                errorWindow.Show();
                return;
            }

            Hall hall = new Hall()
            {
                Number = Convert.ToInt32(Number.Text),
                CountOfSeats = countOfSeats
            };

            Connection.db.Hall.Add(hall);
            Connection.db.SaveChanges();

            int idHall = Connection.db.Hall.Max(item => item.ID);
            int tempNumberOfSeat = 0;
            foreach (UIElement item in Seats.Children)
            {
                tempNumberOfSeat++;
                if (((Button)item).Background == Brushes.Red)
                    continue;

                Places place = new Places()
                {
                    Number = tempNumberOfSeat,
                    IDHall = idHall
                };
                Connection.db.Places.Add(place);
                Connection.db.SaveChanges();
            }

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}