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
    public class Course
    {

        public Course(string crn, string coursename, string buildingname, string roomnumber, string instructorname)
        {
            this.CRN = crn;
            this.courseName = coursename;
            this.buildingName = buildingname;
            this.roomNumber = roomnumber;
            this.instructorName = instructorname;
        }
        public String CRN { get; set; }
        public String courseName { get; set; }
        public String buildingName { get; set; }
        public String roomNumber { get; set; }
        public String instructorName { get; set; }


    }
}