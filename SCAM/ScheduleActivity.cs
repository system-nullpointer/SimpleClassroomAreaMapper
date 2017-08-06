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
    [Activity(Label = "Current Schedule")]
    public class ScheduleActivity : ListActivity
        {
            Course[] items;
            string[] itemName;

            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                Student student = Student.getStudent();



                //populates list for viewSchedule
                // ListAdapter = new ArrayAdapter<Course>(this, Android.Resource.Layout.SimpleListItem1, student.Courses);
                SetContentView(Resource.Layout.viewSchedule);
                List<String> _courseName = new List<String>();

                foreach (var item in student.Courses)
                {
                    _courseName.Add(item.courseName);
                }

                ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, _courseName);


                Button addCourseButton = FindViewById<Button>(Resource.Id.addNewCourseButton);


                addCourseButton.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(addCourseActivity));
                    StartActivity(intent);

                };

            }

            public override void OnBackPressed()
            {
                base.OnBackPressed();
                var intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
                Finish();
            }


        }
    
}