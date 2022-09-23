using System.Linq;
using System.Windows;

namespace Kinomaks
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void AuthorizationClick(object sender, RoutedEventArgs e)
        {
            #region Авторизация
            if (Authorization(Login.Text.ToString(), Password.Password.ToString()))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        public bool Authorization(string login, string password)
        {
            if (Login.Text == "" || Password.Password == "")
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return false;
            }
            if (Connection.db.Users.Select(item => item.Login + " " + item.Password).Contains(Login.Text + " " + Encrypt.Hash(Password.Password)))
            {
                int userID = Connection.db.Users.Where(users => users.Login == Login.Text).Select(users => users.ID).FirstOrDefault();
                int Role = Connection.db.Users.Where(users => users.ID == userID).Select(users => users.IDRole).FirstOrDefault();
                User.Role = Role;
                User.IDUser = userID;
                return true;
            }
            else
            {
                ErrorWindow errorWindow = new ErrorWindow("неверный логин/пароль");
                errorWindow.Show();
                return false;
            }
            #endregion
        }

        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            #region Переход на окно регистрации
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
            #endregion
        }
    }
}