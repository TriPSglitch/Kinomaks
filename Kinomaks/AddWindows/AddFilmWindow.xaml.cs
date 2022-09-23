﻿using System.Windows;
using System.Windows.Media.Imaging;

namespace Kinomaks.AddWindows
{
    /// <summary>
    /// Логика взаимодействия для AddFilmWindow.xaml
    /// </summary>
    public partial class AddFilmWindow : Window
    {
        public AddFilmWindow()
        {
            InitializeComponent();
        }
        private void SelectButtonClick(object sender, RoutedEventArgs e)
        {
            #region Выбор картинки
            BitmapImage image = new BitmapImage();
            image = ImagesManip.SelectImage();
            Logo.Source = image;
            #endregion
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void AddButtonClick(object sender, RoutedEventArgs e)
        {
            #region Добавление фильма
            if (Title.Text == "")
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return;
            }

            Films film = new Films()
            {
                Title = Title.Text
            };

            if (Description.Text != null)
                film.Descripton = Description.Text;

            if (Logo.Source != null)
                film.Logo = ImagesManip.BitmapSourceToByteArray((BitmapSource)Logo.Source);

            Connection.db.Films.Add(film);
            Connection.db.SaveChanges();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
            #endregion
        }
    }
}