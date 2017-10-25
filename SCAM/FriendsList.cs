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

namespace SCAM
{
    [Activity(Label = "FriendsList", ScreenOrientation = ScreenOrientation.Portrait)]
    public class FriendsList : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Student demo = Student.getStudent();
            List<Student> availableStudents = Helper.available;



            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);
            // Create your application here
            SetContentView(Resource.Layout.FriendsListLayout);

            Button addFriendBtn = FindViewById<Button>(Resource.Id.AddFriendBtn);
            addFriendBtn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddFriend));
                StartActivity(intent);
            };

        }
        protected override void OnResume()
        {
            base.OnResume();
            Student demo = Student.getStudent();
            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);
        }
    }
}