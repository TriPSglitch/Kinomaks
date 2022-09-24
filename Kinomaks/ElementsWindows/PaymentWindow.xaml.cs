﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        int idFilm;
        List<int> numberOfSeats;
        int idTimetable;
        int idHall;

        public PaymentWindow(int idFilm, List<int> numberOfSeats, int idTimetable, int idHall)
        {
            InitializeComponent();

            this.idFilm = idFilm;
            this.numberOfSeats = numberOfSeats;
            this.idTimetable = idTimetable;
            this.idHall = idHall;

            ToPay.Content = Connection.db.Films.Where(item => item.ID == this.idFilm).Select(item => item.Price).FirstOrDefault() * this.numberOfSeats.Count();
            Payment();
        }

        void Payment()
        {
            Buyer buyer = new Buyer();
            buyer.DoBuy(new BuyCash(idFilm, numberOfSeats, idTimetable, idHall));
        }

        private void PaidButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            PaymentMethod paymentMethod = new PaymentMethod(idFilm, numberOfSeats, idTimetable, idHall);
            paymentMethod.Show();
            this.Close();
        }
    }
}