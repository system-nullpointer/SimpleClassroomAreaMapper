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
    [Activity(Label = "addCourseActivity")]
    public class addCourseActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.enterCourse);

            Button addButton = FindViewById<Button>(Resource.Id.addToCoursesButton);

            addButton.Click += (sender, e) =>
            {
                var CRN = FindViewById<EditText>(Resource.Id.CRNText).Text;
                var courseName = FindViewById<EditText>(Resource.Id.courseNameText).Text;
                var buildingName = FindViewById<EditText>(Resource.Id.buildingNameText).Text;
                var roomNumber = FindViewById<EditText>(Resource.Id.roomNumberText).Text;
                var instructorName = FindViewById<EditText>(Resource.Id.instructorNameText).Text;


                var Course = new Course(CRN, courseName, buildingName, roomNumber, instructorName);
                Student student;

                student = Student.getStudent();

                student.addCourse(Course);



                var intent = new Intent(this, typeof(ScheduleActivity));
                StartActivity(intent);


            };

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            var intent = new Intent(this, typeof(ScheduleActivity));
            StartActivity(intent);
            Finish();
        }

    }
}