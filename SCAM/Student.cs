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
    public class Student : Person
    {

        public static string currentStudentId { get; set; }

        private bool _isEnabled;

        private static Student s;
        public static Student getStudent()
        {
            if (s == null)
            {
                s = new Student();
                return s;
            }
            else
            {
                return s;
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        private Student()
        {
            Friends = new List<Student>();
            Courses = new List<Course>();
        }

        private List<Student> _friends;

        public List<Student> Friends
        {
            get { return _friends; }
            set { _friends = value; }
        }
        public Student(Student student)
        {
            this.FirstName = student.FirstName;
            this.LastName = student.LastName;
            this.ID = student.ID;
        }

        public Student(string firstName, string lastName, string id)
        {
            FirstName = firstName;
            LastName = lastName;
            ID = id;
            IsEnabled = true;
            Friends = new List<Student>();
            Courses = new List<Course>();
        }

        public void AddFriend(Student student)
        {
            Friends.Add(student);
        }

        //add course to Student courses list
        public void addCourse(Course course)
        {
            //Student s = SingleStudent.getStudent();

            _Courses.Add(course);
        }

        public static Student FindStudentByID(string id, IEnumerable<Student> students)
        {
            Student s = students.FirstOrDefault(o => o.ID == id);
            return s;
        }

        //Students list of courses w/ getters and setters
        private List<Course> _Courses;
        public List<Course> Courses
        {
            get { return _Courses; }
            set { _Courses = value; }
        }


        public static void setFirstName(string name = "New")
        {
            s.FirstName = name;
        }

        public static void setLastName(string lastName = "User")
        {
            s.LastName = lastName;
        }

        public static void setUserID(string id = "0000")
        {
            s.ID = id;
        }


    }
}