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
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using Firebase.Database;
using Firebase.Auth;
using Android.Support.Design.Widget;
using Firebase;
using Android.Content.PM;

namespace SCAM
{
    [Activity(Label = "MessageActivity", Theme = "@style/Theme.AppCompat.Light.NoActionBar", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MessageActivity : AppCompatActivity, IValueEventListener
    {
        private FirebaseClient firebase;
        private List<MessageContent> lstMessage = new List<MessageContent>();
        private ListView lstChat;
        private EditText edtChat;
        private FloatingActionButton fab;

        

        protected override void OnCreate(Bundle savedInstanceState)
        {

            try
            {
                base.OnCreate(savedInstanceState);
                SetContentView(Resource.Layout.Messaging);
                firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
                FirebaseDatabase.Instance.GetReference(FriendsList.currentChatRoom).AddValueEventListener(this);

                fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
                edtChat = FindViewById<EditText>(Resource.Id.input);
                lstChat = FindViewById<ListView>(Resource.Id.list_of_messages);

                fab.Click += delegate
                {
                    PostMessage();
                };
            }
            catch (Exception e)
            {

                Toast.MakeText(this, e.Message, ToastLength.Long).Show();

            }


        }

        private async void PostMessage()
        {
            try
            {
                var items = await firebase.Child(FriendsList.currentChatRoom).PostAsync(new MessageContent(FirebaseAuth.Instance.CurrentUser.Email, edtChat.Text));
                edtChat.Text = "";

            }
            catch (Exception e)
            {

                Toast.MakeText(this, e.Message, ToastLength.Long).Show();

            }

        }

        public void OnCancelled(DatabaseError error)
        {

        }

        public void OnDataChange(DataSnapshot snapshot)
        {
            DisplayChatMessage();
        }

        private async void DisplayChatMessage()
        {


            try
            {
                lstMessage.Clear();
                var items = await firebase.Child(FriendsList.currentChatRoom)
                    .OnceAsync<MessageContent>();

                foreach (var item in items)
                    lstMessage.Add(item.Object);
                ListViewAdapter adapter = new ListViewAdapter(this, lstMessage);
                lstChat.Adapter = adapter;
            }
            catch (Exception e)
            {

                Toast.MakeText(this, e.Message, ToastLength.Long).Show();

            }

        }


    }
}