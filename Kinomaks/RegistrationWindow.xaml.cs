using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Kinomaks
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        public RegistrationWindow()
        {
            InitializeComponent();
        }
        private void RegistrationClick(object sender, RoutedEventArgs e)
        {
            #region Регистрация
            bool regRes = Registration(Login.Text.ToString(), Password.Password.ToString(), FirstName.Text.ToString(),
                                        SecondName.Text.ToString(), MiddleName.Text.ToString(), Email.Text.ToString());

            if (regRes)
            {
                #region Результат и переход на окно авторизации
                RegitrationConfirmedWindow regitrationConfirmedWindow = new RegitrationConfirmedWindow();
                regitrationConfirmedWindow.Show();

                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                this.Close();
                #endregion
            }
        }

        public bool Registration(string login, string password, string Fname, string Sname, string Mname, string email)
        {
            #region Валидация
            if (Login.Text == "" || Password.Password == "" || FirstName.Text == "" || SecondName.Text == "" || Email.Text == "")
            {
                ErrorWindow errorWindow = new ErrorWindow("пустые поля");
                errorWindow.Show();
                return false;
            }
            if (Connection.db.Users.Select(item => item.Login).Contains(Login.Text))
            {
                ErrorWindow errorWindow = new ErrorWindow("такой пользователь уже существует");
                errorWindow.Show();
                return false;
            }
            if (!IsValidEmail(Email.Text))
            {
                ErrorWindow errorWindow = new ErrorWindow("неверный формат почты");
                errorWindow.Show();
                return false;
            }
            #endregion


            #region Добавление пользователя
            Users user = new Users()
            {
                Login = Login.Text,
                Password = Encrypt.Hash(Password.Password),
                SecondName = SecondName.Text,
                FirstName = FirstName.Text,
                Email = Email.Text,
                IDRole = 2
            };
            if (MiddleName.Text != "")
            {
                user.MiddleName = MiddleName.Text;
            }

            Connection.db.Users.Add(user);
            Connection.db.SaveChanges();

            return true;
            #endregion

            #endregion
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            #region Назад
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
            #endregion
        }

        private bool IsValidEmail(string email)
        {
            #region Валидация
            string regex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            if (Regex.IsMatch(email, regex))
                return true;
            else
                return false;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Regex.IsMatch((((TextBox)sender).Text).ToString(), "[^А-я-:]"))
            {
                ((TextBox)sender).Text = ((TextBox)sender).Text.Remove(((TextBox)sender).Text.Length - 1);
                ((TextBox)sender).SelectionStart = ((TextBox)sender).Text.Length;
            }
            #endregion
        }
    }
}