using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase.Xamarin.Database;
using System.Collections.Generic;
using Firebase.Database;
using System;
using Firebase.Auth;
using Android.Content;
using Android.Runtime;
using Android.Support.Design.Widget;
using Firebase;

namespace SCAM
{
    
    [Activity(Label = "SCAM", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity//, IValueEventListener
    {
        
        private FirebaseClient firebase;

        public int MyResultCode = 1;

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Keep this here, should prevent Firebase not initialized error that's been causing us to clean/rebuild
            FirebaseApp.InitializeApp(this);
            //updated this to SignIn instead of Main
            SetContentView(Resource.Layout.Main);

            firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));

            List<Student> availableStudents = Helper.createStudents();
            Button campusMapButton = FindViewById<Button>(Resource.Id.campusMap);
            Button friendsButton = FindViewById<Button>(Resource.Id.friends);
            Button messagesButton = FindViewById<Button>(Resource.Id.messages);
            Button adminButton = FindViewById<Button>(Resource.Id.admin);
            Button viewScheduleButton = FindViewById<Button>(Resource.Id.viewSchedule);

            friendsButton.Click += (sender, e) =>
            {

                var intent = new Intent(this, typeof(FriendsList));
                StartActivity(intent);
            };

            adminButton.Click += (sender, e) =>
            {
                var uri = Android.Net.Uri.Parse("https://accounts.google.com/signin/v2/identifier?passive=true&continue=https%3A%2F%2Ffirebase.google.com%2F%3Frefresh%3D1&service=ahsid&flowName=GlifWebSignIn&flowEntry=ServiceLogin");

                var intent = new Intent(Intent.ActionView, uri);
                StartActivity(intent);
            };


            messagesButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(MessageActivity));
                StartActivity(intent);
            };

            campusMapButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(CampusMap));
                StartActivity(intent);
            };


            //handling on click of view schedule button(sending to ScheduleActivity)
            viewScheduleButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ScheduleActivity));
                StartActivity(intent);

            };



            //if (FirebaseAuth.Instance.CurrentUser == null)
            StartActivityForResult(new Android.Content.Intent(this, typeof(SignIn)), MyResultCode);
            /*
            else
            {  
                //Removed this piece as this is moving into the messagins portion of the app.
                //DisplayChatMessage();
                StartActivityForResult(new Android.Content.Intent(this, typeof(SignIn)), MyResultCode);
            }
            */   
        }
        
        public void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.Main);
        }

    

        
    }
}

