using System.Collections.Generic;
using System.Windows;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для PaymentMethod.xaml
    /// </summary>
    public partial class PaymentMethod : Window
    {
        int idFilm;
        List<int> numberOfSeats;
        int idTimetable;
        int idHall;

        public PaymentMethod(int idFilm, List<int> numberOfSeats, int idTimetable, int idHall)
        {
            InitializeComponent();

            this.idFilm = idFilm;
            this.numberOfSeats = numberOfSeats;
            this.idTimetable = idTimetable;
            this.idHall = idHall;
        }

        private void CashButtonClick(object sender, RoutedEventArgs e)
        {
            PaymentWindow paymentWindow = new PaymentWindow(idFilm, numberOfSeats, idTimetable, idHall);
            paymentWindow.Show();
            this.Close();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            BuyingATicket buyingATicket = new BuyingATicket(idTimetable);
            buyingATicket.Show();
            this.Close();
        }
    }
}