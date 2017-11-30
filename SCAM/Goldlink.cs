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
    class Goldlink
    {
        public Goldlink()
        {
            buildCourseList();
        }
        public Dictionary<string, Course> CourseDictionary { get; set; }
        private void buildCourseList()
        {
            CourseDictionary = new Dictionary<string, Course>()
            {
                { "CSCI 1250-001", new Course("CSCI 1250-001", "Nicks", "TR", "12/14/17", "11:20 AM",
                    "Bailey", "Intro to Java I", "103", "8/21/17", "9:20 AM") },
                { "CSCI 1260-001", new Course("CSCI 1260-001", "Nicks", "MWF", "12/14/17", "11:00 AM",
                        "Bailes", "Intro to Java II", "103", "8/21/17", "9:00 AM")  },
                { "CSCI 4700-001", new Course("CSCI 4700-001", "Nicks", "MWF", "12/14/17", "2:45 PM",
                    "Tarnoff", "Server Side Web Programming", "460", "8/21/17", "12:45 PM" ) },
                { "CSCI 4800-001", new Course("CSCI 4800-001", "Nicks", "MW", "12/14/17", "4:45 PM",
                    "Robinson", "Senior Project", "104", "8/21/17", "2:45 PM" ) },
                { "CSCI 2210-002", new Course("CSCI 2210-002", "Nicks", "MW", "12/14/17", "10:00 AM",
                    "Bailes", "Data Structures", "102", "8/21/17", "8:00 AM" ) },
                { "ECON 2220-001", new Course("ECON 2220-001", "Sam-Wilson", "MW", "12/14/17", "2:45 PM",
                    "Johnson", "Principles of Microeconmics", "110", "8/21/17", "12:45 PM" ) },
                { "MATH 1720-002", new Course("MATH 1720-002", "Wilson-Wallace", "TR", "12/16/17", "4:45 PM",
                    "Mathison", "Calculus 2", "305", "8/23/17", "2:45 PM" ) },
                { "MATH 4267-002", new Course("MATH 4267-02", "Gilbreath Hall", "MWF", "12/14/17", "11:20 AM",
                    "D. Kinsley", "Linear Algebra", "226", "8/21/17", "9:20 AM" ) }
            };

        }//end buildCourseList
    }//end Goldlink
}
