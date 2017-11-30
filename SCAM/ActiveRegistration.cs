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
    class ActiveRegistration
    {
        public ActiveRegistration()
        {
            buildCurrentRegistration();
        }

        public List<Course> Courses { get; set; }
        

        private void buildCurrentRegistration()
        {
            Courses = new List<Course>()
            {
                {new Course( "CSCI 4717-001", "Nicks", "MW", "12/14/17", "4:45 PM", "Tarnoff", "Computer Architecture",
                                "235", "8/21/17", "2:45 PM" ) },
                {new Course( "CSCI 3350-001", "Nicks", "TR", "12/14/17", "2:05 PM", "Bennett", "Software Engineering II",
                                "132", "8/21/17", "12:05 PM" ) }
            };
        }
    }
}