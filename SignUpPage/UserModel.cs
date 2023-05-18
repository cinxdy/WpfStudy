using System;
using System.ComponentModel;

namespace WpfStudy.Hw1
{
    public class UserModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public UserModel() { }

        public UserModel(UserData user)
        {
            UserEmail = user.UserEmail;
            Password = user.Password;
            UserName = user.Name;
            BirthdayDate = user.birthday;
            SexualityNum = user.sexuality;
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _id;
        public string Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        private string _mailDomain;
        public string MailDomain
        {
            get => _mailDomain;
            set
            {
                _mailDomain = value;
                OnPropertyChanged(nameof(MailDomain));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _passwordCheck;
        public string PasswordCheck
        {
            get => _passwordCheck;
            set
            {
                _passwordCheck = value;
                OnPropertyChanged(nameof(PasswordCheck));
                if (PasswordCheck != Password)
                {
                    IsValidParam = false;
                }
            }
        }

        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _userEmail;
        public string UserEmail
        {
            get => _userEmail ?? $@"{Id}@{MailDomain}";
            set
            {
                _userEmail = value;
                OnPropertyChanged(nameof(UserName));
            }
        } 

        private int? _birthdayNum;
        public int? BirthdayNum
        {
            get => _birthdayNum;
            set
            {
                _birthdayNum = value;
                OnPropertyChanged(nameof(BirthdayNum));
                setBirthday();
            }
        }

        private int? _sexualityNum;
        public int? SexualityNum
        {
            get => _sexualityNum;
            set
            {
                _sexualityNum = value;
                OnPropertyChanged(nameof(SexualityNum));

                if (SexualityNum == 1 || SexualityNum == 3)
                {
                    IsMan = true;
                    SexualityStr = "남자";
                }
                else if (SexualityNum == 2 || SexualityNum == 4)
                {
                    IsWoman = true;
                    SexualityStr = "여자";
                }
                else
                {
                    IsValidParam = false;
                }

                setBirthday();
            }
        }

        private void setBirthday()
        {
            if (BirthdayNum == null || SexualityNum == null) return;

            int year = BirthdayNum.Value / 10000 % 100;
            if (SexualityNum < 3) year += 1900;
            else year += 2000;

            int month = BirthdayNum.Value / 100 % 100;
            int day = BirthdayNum.Value % 100;
            try
            {
                BirthdayDate = new DateTime(year, month, day);
                IsValidParam = true;
            }
            catch (ArgumentOutOfRangeException)
            {
                IsValidParam = false;
            }
        }

        private string _sexualityStr;
        public string SexualityStr
        {
            get => _sexualityStr;
            set
            {
                _sexualityStr = value;
                OnPropertyChanged(nameof(SexualityStr));
            }
        }

        private DateTime _birthdayDate;
        public DateTime BirthdayDate
        {
            get => _birthdayDate;
            set
            {
                _birthdayDate = value;
                OnPropertyChanged(nameof(BirthdayDate));
            }
        }

        private bool _isMan;
        public bool IsMan
        {
            get => _isMan;
            set
            {
                _isMan = value;
                OnPropertyChanged(nameof(IsMan));
            }
        }

        private bool _isWoman;
        public bool IsWoman
        {
            get => _isWoman;
            set
            {
                _isWoman = value;
                OnPropertyChanged(nameof(IsWoman));
            }
        }

        private bool _isValidParam;
        public bool IsValidParam
        {
            get => _isValidParam;
            set
            {
                _isValidParam = value;
                OnPropertyChanged(nameof(IsValidParam));
            }
        }

    }
}
