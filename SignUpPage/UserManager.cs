using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfStudy.Hw1
{
    internal static class UserManager
    {
        private static readonly List<UserData> _users = new List<UserData>();

        public static bool ExistOne(string userEmail)
        {
            return _users.Any(user => user.UserEmail == userEmail);
        }

        public static int SignIn(string userEmail, string password)
        {
            try
            {
                return _users.FindIndex(user => user.UserEmail == userEmail && user.Password == password);
            } 
            catch 
            {
                return -1; 
            }
        }

        public static UserData GetUser(int uid)
        {
            return _users[uid];
        }

        public static bool AddOne(string userEmail, string password, string name, DateTime birthday, int sexuality)
        {
            if (ExistOne(userEmail)) return false;

            _users.Add(new UserData { UserEmail = userEmail, Password = password, Name = name, birthday = birthday, sexuality = sexuality });
            return true;
        }
    }

    public class UserData
    {
        public string UserEmail;
        public string Password;
        public string Name;
        public DateTime birthday;
        public int sexuality; // 1: man 2: woman
    }
}
