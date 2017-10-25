using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using System.Collections.Generic;
using Firebase.Database;
using System;
using Firebase.Auth;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Firebase;
using System.Linq;

namespace SCAM
{
    public static class Helper
    {
        private static Student _user;

        public static Student User
        {
            get { return Student.getStudent(); }
            set { _user = value; }
        }

        public static List<Student> available { get; set; }
        public static Student createUser(string first, string last, string ID)
        {
            Student.setFirstName(first);
            Student.setLastName(last);
            Student.setUserID(ID);
            User = Student.getStudent();
            return User;
        }

        public static List<Student> createStudents()
        {
            available = new List<Student>(10);

            available.Add(new Student("Cody", "Morgan", "0001"));
            available.Add(new Student("Andrew", "Bathon", "0002"));
            available.Add(new Student("Michael", "Acero", "0003"));
            available.Add(new Student("Matt", "Taylor", "0004"));
            available.Add(new Student("Mathes", "Sentell", "0005"));

            return available;
        }

        
    }
}