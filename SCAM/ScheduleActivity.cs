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
using Newtonsoft.Json;

namespace SCAM
{
    [Activity(Label = "Current Schedule")]
    public class ScheduleActivity : ListActivity
        {
            
            Student student = Student.getStudent();
            Button addNewCourseButton;
            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);

                //populates list for viewSchedule
                // ListAdapter = new ArrayAdapter<Course>(this, Android.Resource.Layout.SimpleListItem1, student.Courses);
                SetContentView(Resource.Layout.viewSchedule);

                /*List<String> courses = new List<String>();

                foreach (var item in student.Courses)
                {
                    courses.Add(item.ToString());
                }
                */
                ListAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, student.Courses);
           

                addNewCourseButton = FindViewById<Button>(Resource.Id.addNewCourseButton);    

            
                addNewCourseButton.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(addCourseActivity));
                    //intent.PutExtra("Student", JsonConvert.SerializeObject(student));
                    StartActivity(intent);
                };               

            }
        
        protected override void OnResume()
        {
            
            ListAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, student.Courses);
            SetContentView(Resource.Layout.viewSchedule);
            addNewCourseButton = FindViewById<Button>(Resource.Id.addNewCourseButton);
        }

        public override void OnBackPressed()
        {
            StartActivity(typeof(MainActivity));
        }
 
    }

}