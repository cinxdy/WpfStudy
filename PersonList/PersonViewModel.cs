using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PersonList
{
    internal class PersonViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<PersonModel> _persons;

        public ObservableCollection<PersonModel> Persons
        {
            get
            {
                if (_persons == null)
                    _persons = new ObservableCollection<PersonModel>();
                return _persons;
            }
        }

        public PersonModel selectedItem { get; set; }

        public PersonViewModel()
        {
            _persons = new ObservableCollection<PersonModel>();
            _persons.Add(new PersonModel("jiyoung", 25, "Seoul", "010-4296-8301"));
        }

        private void Add()
        {
            var modal = new SignUpModal();
            modal.ShowDialog();

            if (modal.newPerson == null)
                MessageBox.Show("No Person!");
            else
                _persons.Add(modal.newPerson);
        }

        private void Delete()
        {
            var person = selectedItem as PersonModel;
            _persons.Remove(person);
        }

        private void DeleteAll()
        {
            _persons?.Clear();
        }

        private void LoadInfo()
        {
            var person = selectedItem as PersonModel;
            var modal = new SignUpModal(person);
            modal.ShowDialog();
        }

        #region Command
        private ICommand _addCommand;
        public ICommand AddCommand
        {
            get
            {
                if (_addCommand == null)
                    _addCommand = new DelegateCommand(Add);
                return _addCommand;
            }
        }

        private ICommand _deleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_deleteCommand == null)
                    _deleteCommand = new DelegateCommand(Delete);
                return _deleteCommand;
            }
        }

        private ICommand _deleteAllCommand;
        public ICommand DeleteAllCommand
        {
            get
            {
                if (_deleteAllCommand == null)
                    _deleteAllCommand = new DelegateCommand(DeleteAll);
                return _deleteAllCommand;
            }
        }

        private ICommand _loadInfoCommand;
        public ICommand LoadInfoCommand
        {
            get
            {
                if (_loadInfoCommand == null)
                    _loadInfoCommand = new DelegateCommand(LoadInfo);
                return _loadInfoCommand;
            }
        }

        #endregion

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }

}
