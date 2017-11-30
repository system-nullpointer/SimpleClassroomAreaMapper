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
    [Activity(Label = "Add Course Activity")]
    public class addCourseActivity : Activity
    {
        Student student;
        
        ActiveRegistration currentCourses = new ActiveRegistration();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.enterCourse);
            //student = JsonConvert.DeserializeObject<Student>(Intent.GetStringExtra("Student"));
            student = Student.getStudent();

            Button addButton = FindViewById<Button>(Resource.Id.addCourseButton),
                    importButton = FindViewById<Button>(Resource.Id.importCoursesButton);

            //custom course add by student
            addButton.Click += (sender, e) =>
            {
                Goldlink goldlinkDB = new Goldlink();
                
                string courseID = FindViewById<EditText>(Resource.Id.CourseIDText).Text;
                


                if (goldlinkDB.CourseDictionary.ContainsKey(courseID.ToUpper()))
                {
                    student.addCourse(new Course(goldlinkDB.CourseDictionary[courseID]));
                    Toast.MakeText(this, "Course added successfully!", ToastLength.Short).Show();

                    StartActivity(typeof(ScheduleActivity));
                }
                else
                {
                    new AlertDialog.Builder(this)
                            .SetTitle("Error")
                            .SetMessage("Course Not Found!")
                            .SetPositiveButton("OK", delegate { this.Finish(); })
                            .Show();
                    StartActivity(typeof(ScheduleActivity));
                }

                
                

            };
            //import active registration from Goldlink
            importButton.Click += (sender, e) =>
            {
                foreach (Course course in currentCourses.Courses)
                {
                    student.addCourse(course);
                }
                Toast.MakeText(this, $"{currentCourses.Courses.Count} course(s) imported successfulyl!", ToastLength.Short).Show();
                StartActivity(typeof(ScheduleActivity));

            };

        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            //StartActivity(typeof(ScheduleActivity));
            Finish();
        }
        
    }
}