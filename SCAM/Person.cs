using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace SCAM
{
    public abstract class Person
    {
        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        private string _id;
        private string _email;

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public Person()
        {

        }
        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}