using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace WpfStudy.Hw1
{
    /// <summary>
    /// Interaction logic for SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage(UserModel userModel = null)
        {
            InitializeComponent();
            SignUpRoot.DataContext = userModel ?? new UserModel();

            Loaded += SignUpPage_Loaded;
        }

        private void SignUpPage_Loaded(object sender, RoutedEventArgs e)
        {
            App.Current.MainWindow.Title = "Sign Up Page";
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            var userModel = SignUpRoot.DataContext as UserModel;

            var userEmail = userModel.UserEmail;
            var res = UserManager.AddOne(userEmail, userModel.Password, userModel.UserName, userModel.BirthdayDate, userModel.IsMan ? 1 : 2);

            if (res)
            {
                NavigationService.Navigate(new LoginPage(userModel));
                MessageBox.Show("Successfully Signed up");
            }
            else
            {
                MessageBox.Show("Something's wrong");
            }
        }
    }
}
