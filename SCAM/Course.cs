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
        public String CourseID { get; set; }  //includes course number and section
        public String CourseTitle { get; set; }
        public String BuildingName { get; set; }
        public String RoomNumber { get; set; }
        public String InstructorName { get; set; }
        public String DaysHeld { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String StartDate { get; set; }
        public String EndDate { get; set; }

        public Course(string courseID, string courseTitle, string instructorName, string bldgName, 
            string roomNumber, string days, string startTime, string endTime,
            string startDate, string endDate)
        {
            CourseID = courseID;
            CourseTitle = courseTitle;
            InstructorName = instructorName;
            BuildingName = bldgName;
            RoomNumber = roomNumber;
            DaysHeld = days;
            StartTime = startTime;
            EndTime = endTime;
            StartDate = startDate;
            EndDate = endDate;
        }

        public override string ToString()
        {
            string courseOutput = "";
            courseOutput += $"\nCourse ID: {CourseID}"
                        + $"\nCourse Title: {CourseTitle}"
                        + $"\nInstructor: {InstructorName}"
                        + $"\nBuilding: {BuildingName}"
                        + $"\nRoom: {RoomNumber}"
                        + $"\nDays: {DaysHeld}"
                        + $"\nTime: {StartTime} - {EndTime}"
                        + $"\nStartDate: {StartDate}"
                        + $"\nEnd Date: {EndDate}";

            return courseOutput;
        }
    }
}