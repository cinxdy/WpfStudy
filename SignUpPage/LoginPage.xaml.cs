using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfStudy.Hw1
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage() {
            InitializeComponent();
            LoginPageRoot.DataContext = new UserModel();

            Loaded += LoginPage_Loaded                ;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Title = "Login Page";
        }

        public LoginPage(UserModel userModel = null)
        {
            InitializeComponent();
            LoginPageRoot.DataContext = userModel ?? new UserModel();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var dc = LoginPageRoot.DataContext as UserModel;

            var userEmail = dc.UserEmail;
            var password = dc.Password;

            if (!UserManager.ExistOne(userEmail))
            {
                MessageBox.Show("There's no user who has the user email");
                btnSignUp.Visibility = Visibility.Visible;
                return;
            }

            var uid = UserManager.SignIn(userEmail, password);
            if (uid >= 0)
            {
                MessageBox.Show("Successfully Logined!");
                var user = UserManager.GetUser(uid);
                var userModel = new UserModel(user);
                NavigationService.Navigate(new InfoPage(userModel));
            }
            else
            {
                MessageBox.Show("Check your id and password again!");
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var dc = LoginPageRoot.DataContext as UserModel;
            var signUpPage = new SignUpPage(dc);
            NavigationService.Navigate(signUpPage);
        }
    }
}
