using System;
using System.Windows;

namespace PersonList
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SignUpModal : Window
    {
        public SignUpModal()
        {
            InitializeComponent();
            DataContext = new PersonModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.newPerson = new PersonModel(TxtName.Text, Int32.Parse(TxtAge.Text), TxtAddress.Text, TxtPhoneNumber.Text);
            this.Close();
        }

        internal PersonModel newPerson { get; private set; }
    }
}
