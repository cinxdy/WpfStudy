using System.ComponentModel;

namespace PersonList
{
    internal class PersonModel
    {
        public PersonModel()
        {

        }

        public PersonModel(string name, int age, string address, string phoneNumber)
        {
            this.Name = name;
            this.Age = age;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
        }

        #region Properties 

        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        #endregion
    }
}
