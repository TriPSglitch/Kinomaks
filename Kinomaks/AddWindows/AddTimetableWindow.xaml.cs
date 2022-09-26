using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace Kinomaks.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddTimetable.xaml
    /// </summary>
    public partial class AddTimetableWindow : Window
    {

        public AddTimetableWindow()
        {
            InitializeComponent();
            Hall.ItemsSource = Connection.db.Hall.Select(item => item.Number).ToList();
            Film.ItemsSource = Connection.db.Films.Select(items => items.Title).ToList();
        }
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление расписания
            if (Date.Text == "" || Time.Text == "" || Hall.SelectedItem == null || Film.SelectedItem == null)
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return;
            }

            TimeSpan timeResult;
            if (!TimeSpan.TryParse(Time.Text, out timeResult) || Time.Text.Count(item => item == ':') != 2)
            {
                ErrorWindow errorWindow = new ErrorWindow("неверно указано время");
                errorWindow.Show();
                return;
            }

            string year, month, day;
            day = Date.Text.Substring(0, Date.Text.IndexOf('.'));
            month = Date.Text.Substring(Date.Text.IndexOf('.') + 1, 2);
            year = Date.Text.Substring(Date.Text.LastIndexOf('.') + 1, Date.Text.Length - 1 - Date.Text.LastIndexOf('.'));
            string correctDate = year + '.' + month + '.' + day;

            DateTime dateResult;
            if (!DateTime.TryParse(correctDate, out dateResult) || correctDate.Count(item => item == '.') != 2)
            {
                ErrorWindow errorWindow = new ErrorWindow("неверно указана дата");
                errorWindow.Show();
                return;
            }

            Hall selectedHall = Connection.db.Hall.Where(item => item.Number == (int)Hall.SelectedItem).FirstOrDefault();

            if (Connection.db.Timetable.Select(item => item.Time + " " + item.Date + " " + item.IDHall).Contains(timeResult + " " + dateResult + " " +
                                            selectedHall.ID))
            {
                ErrorWindow errorWindow = new ErrorWindow("зал уже занят");
                errorWindow.Show();
                return;
            }

            Timetable timetable = new Timetable()
            {
                Time = timeResult,
                Date = dateResult,
                IDHall = selectedHall.ID
            };

            Connection.db.Timetable.Add(timetable);
            Connection.db.SaveChanges();

            Films selectedFilm = Connection.db.Films.Where(item => item.Title == Film.SelectedItem.ToString()).FirstOrDefault();

            FilmTimetable filmTimetable = new FilmTimetable()
            {
                IDFilm = selectedFilm.ID,
                IDTimeTable = Connection.db.Timetable.Max(item => item.ID)
            };

            Connection.db.FilmTimetable.Add(filmTimetable);
            Connection.db.SaveChanges();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            #endregion
        }

        private void DateTextChanged(object sender, TextChangedEventArgs e)
        {
            #region Валидация
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^0-9.]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
        }

        private void TimeTextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^0-9:]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
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