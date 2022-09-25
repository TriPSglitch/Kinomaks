using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kinomaks.ElementsWindows
{
    /// <summary>
    /// Логика взаимодействия для ReturnWindow.xaml
    /// </summary>
    public partial class ReturnWindow : Window
    {
        public ReturnWindow()
        {
            InitializeComponent();
        }

        private void ReturnButtonClick(object sender, RoutedEventArgs e)
        {
            int cheque;
            if (!int.TryParse(Cheque.Text, out cheque))
            {
                ErrorWindow errorWindow = new ErrorWindow("неверный формат чека");
                errorWindow.Show();
                return;
            }

            DateTime now = DateTime.Now;
            DateTime date = Connection.db.UserTicket.Where(item => item.ID == cheque).Select(item => item.Timetable.Date).FirstOrDefault();
            TimeSpan time = Connection.db.UserTicket.Where(item => item.ID == cheque).Select(item => item.Timetable.Time).FirstOrDefault();

            if (now.Date >= date || (now.Date == date && now.TimeOfDay > time))
            {
                ErrorWindow errorWindow = new ErrorWindow("вы уже не можете вернуть этот билет");
                errorWindow.Show();
                return;
            }    

            UserTicket userTicket = Connection.db.UserTicket.Where(item => item.ID == cheque).Select(item => item).FirstOrDefault();

            if (userTicket == null)
            {
                ErrorWindow errorWindow = new ErrorWindow("такого билета не существует");
                errorWindow.Show();
                return;
            }

            Connection.db.UserTicket.Remove(userTicket);

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
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
