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
        private const int SendMessage = 2;
        private const int RemoveFriend = 1;

        private List<Student> availableStudents;

        private Student demo;

        public List<Student> AvailableStudents
        {
            get { return availableStudents; }
            set { availableStudents = value; }
        }

        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            demo = Student.getStudent();

            FriendPosition = -1;

            availableStudents = Helper.available;

            ListAdapter = new ArrayAdapter<Student>(this, Android.Resource.Layout.SimpleListItem1, demo.Friends);


            // Create your application here
            SetContentView(Resource.Layout.FriendsListLayout);

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