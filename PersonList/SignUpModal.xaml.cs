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

        internal SignUpModal(PersonModel person)
        {
            InitializeComponent();
            DataContext = person;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.newPerson = DataContext as PersonModel;
            this.Close();
        }

        internal PersonModel newPerson { get; private set; }
    }
}
