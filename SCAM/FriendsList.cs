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
using Firebase;
using Firebase.Xamarin.Auth;

namespace SCAM
{
    [Activity(Label = "FriendsList")]
    public class FriendsList : ListActivity
    {
        public static string currentChatRoom;
        private ListView friendsList;
        //AlertDialog alert = new AlertDialog.Builder().Create();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Student demo = Student.getStudent();
            List<Student> availableStudents = Helper.available;
            //friendsList = FindViewById<ListView>(Android.Resource.Id.List);


            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);

            SetContentView(Resource.Layout.FriendsListLayout);
            friendsList = FindViewById<ListView>(Android.Resource.Id.List);
            friendsList.ItemClick += FriendsList_ItemClick;

            Button addFriendBtn = FindViewById<Button>(Resource.Id.AddFriendBtn);
            addFriendBtn.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(AddFriend));
                StartActivity(intent);
            };

        }

        private void FriendsList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {

            List<Student> studentList = Helper.available;

            var vItem = friendsList.GetItemAtPosition(e.Position);
            String emailMessage = Helper.getEmail(vItem.ToString());
            if (emailMessage.Contains("."))
            {
                emailMessage = emailMessage.Replace(".", "");
            }
            String emailCurrent = SignIn.currentUserEmail;
            if (emailCurrent.Contains("."))
            {
                emailCurrent = emailCurrent.Replace(".", "");
            }
            int order = emailCurrent.CompareTo(emailMessage);
            if (order >= 0)
            {
                currentChatRoom = emailCurrent + emailMessage;
            }
            else
            {
                currentChatRoom = emailMessage + emailCurrent;
            }


            StartActivity(typeof(MessageActivity));



        }

        protected override void OnResume()
        {
            base.OnResume();
            Student demo = Student.getStudent();
            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);
        }
    }
}