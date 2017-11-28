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
        private const int SendMessage = 2;
        private const int RemoveFriend = 1;

        private List<Student> availableStudents;

        private Student demo;
        public static string currentChatRoom;
        private ListView friendsList;

        public List<Student> AvailableStudents
        {
            get { return availableStudents; }
            set { availableStudents = value; }
        }

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);


            FriendPosition = -1;

            demo = Student.getStudent();
            List<Student> availableStudents = Helper.available;
            //friendsList = FindViewById<ListView>(Android.Resource.Id.List);

            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);

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

            RegisterForContextMenu(ListView);


        }

        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            menu.SetHeaderTitle("Friend Options");
            FriendPosition = ((AdapterView.AdapterContextMenuInfo)(menuInfo)).Position;

            menu.Add(0, FriendsList.SendMessage, 0, "Send Message");
            menu.Add(0, FriendsList.RemoveFriend, 0, "Remove");

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

        public int FriendPosition { get; set; }
        public override bool OnContextItemSelected(IMenuItem item)
        {
            ArrayAdapter<Student> a = ListAdapter as ArrayAdapter<Student>;
            switch (item.ItemId)
            {
                case SendMessage:
                    Toast.MakeText(this.ApplicationContext, "Direct messaging is not yet implemented", ToastLength.Short).Show();
                    return true;
                case RemoveFriend:
                    if (FriendPosition < a.Count && FriendPosition >= 0)
                    {
                        demo.Friends.RemoveAt(FriendPosition);
                        a.Remove(a.GetItem(FriendPosition));
                        return true;
                    }
                    return false;
                default:
                    return base.OnContextItemSelected(item);
            }
        }




        protected override void OnResume()
        {
            base.OnResume();
            Student demo = Student.getStudent();
            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);
        }




    }
}