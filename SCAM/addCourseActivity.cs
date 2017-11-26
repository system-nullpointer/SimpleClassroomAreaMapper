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
using Android.Content.PM;
using Firebase.Database;

namespace SCAM
{
    [Activity(Label = "Add Course Activity", ScreenOrientation = ScreenOrientation.Portrait)]
    public class addCourseActivity : Activity
    {
        private DatabaseReference _database;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            _database = FirebaseDatabase.Instance.Reference;

            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.enterCourse);

            Button addButton = FindViewById<Button>(Resource.Id.addToCoursesButton);

            addButton.Click += (sender, e) =>
            {
                var courseID = FindViewById<EditText>(Resource.Id.CRNText).Text;
                //var courseTitle = FindViewById<EditText>(Resource.Id.courseNameText).Text;
                //String instructorName = FindViewById<EditText>(Resource.Id.instructorNameText).Text;
                //var buildingName = FindViewById<EditText>(Resource.Id.buildingNameText).Text;
                //var roomNumber = FindViewById<EditText>(Resource.Id.roomNumberText).Text;
                //string days = FindViewById<EditText>(Resource.Id.daysHeldText).Text;
                //string time = FindViewById<EditText>(Resource.Id.timeText).Text;

                Query query = _database.Child("Course").Child(courseID);
                query.Class.GetFields();

                //var Course = new Course(courseID, courseTitle, instructorName, buildingName, roomNumber, days, time);
                Student student;

                student = Student.getStudent();

                student.addCourse(Course);

                //var intent = new Intent(this, typeof(ScheduleActivity));
                //StartActivity(intent);
                Finish();

            };

        }
/*
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            //var intent = new Intent(this, typeof(ScheduleActivity));
            //StartActivity(intent);
            Finish();
        }
*/
    }
}