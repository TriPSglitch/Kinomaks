using System;
using System.Linq;
using System.Windows;

namespace Kinomaks.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddCinemaWindwow.xaml
    /// </summary>
    public partial class AddCinemaWindwow : Window
    {
        public AddCinemaWindwow()
        {
            InitializeComponent();
        }
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление расписания
            if (Name.Text == "" || City.Text == "" || Street.Text == "" || Building.Text == "")
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return;
            }

            if (Connection.db.Cinema.Select(item => item.Name).Contains(Name.Text))
            {
                ErrorWindow errorWindow = new ErrorWindow("кинотеатр с таким названием уже существует");
                errorWindow.Show();
                return;
            }

            if (Connection.db.Cinema.Select(item => item.City + " " + item.Street + " " + item.Building + " ").Contains(City.Text + " " + Street.Text + " " + Building.Text))
            {
                ErrorWindow errorWindow = new ErrorWindow("кинотеатр в этом месте уже существует");
                errorWindow.Show();
                return;
            }

            Cinema cinema = new Cinema()
            {
                Name = Name.Text,
                City = City.Text,
                Street = Street.Text,
                Building = Building.Text
            };

            Connection.db.Cinema.Add(cinema);
            Connection.db.SaveChanges();

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