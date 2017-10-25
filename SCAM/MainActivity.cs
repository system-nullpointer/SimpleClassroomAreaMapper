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
using Android.Content.PM;
using System.Linq;

namespace SCAM
{
    
    [Activity(Label = "SCAM", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity//, IValueEventListener
    {
        private FirebaseClient firebase;
        private DatabaseReference _database;
        private List<string> _databaseKeyValue;


        public int MyResultCode = 1;
        private bool isSignedIn = false;

       

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //updated this to SignIn instead of Main
            SetContentView(Resource.Layout.Main);

            List<Student> availableStudents = Helper.createStudents();
            Button campusMapButton = FindViewById<Button>(Resource.Id.campusMap);
            Button friendsButton = FindViewById<Button>(Resource.Id.friends);
            Button messagesButton = FindViewById<Button>(Resource.Id.messages);
            Button adminButton = FindViewById<Button>(Resource.Id.admin);

            if (FirebaseAuth.Instance.CurrentUser == null)
                StartActivityForResult(new Android.Content.Intent(this, typeof(SignIn)), MyResultCode);

            if (SignIn.isSignedIn == false)
            {
                firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
            }
            FirebaseApp.InitializeApp(this);

            _database = FirebaseDatabase.Instance.Reference;
            // _database.Child("users").Child("Andrew").AddListenerForSingleValueEvent(this);

            if (IsAdmin(_database,"E00402949"))
            {
                adminButton.Visibility = Android.Views.ViewStates.Visible;
                adminButton.Clickable = true;
            }

            //DataSnapshot.

            

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

            //button object of view schedule
            Button viewScheduleButton = FindViewById<Button>(Resource.Id.viewSchedule);

            //handling on click of view schedule button(sending to ScheduleActivity)
            viewScheduleButton.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ScheduleActivity));
                StartActivity(intent);

            };


           
           
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }


        public void OnDataChange(DataSnapshot snapshot)
        {
            GetChildren(snapshot);
        }


        public void OnCancelled(DatabaseError error)
        {
            //Not sure what to do about OnCancelled
            throw new NotImplementedException();
        }

        public void GetChildren(DataSnapshot snapshot)
        {
            if (snapshot == null) return;

            var snapshotChildren = snapshot.Children;
            List<string> childValues = new List<string>();

            //Go through each child on the key
            foreach (DataSnapshot s in snapshotChildren.ToEnumerable())
            {
                //Get the values for the child attributes from the database
                childValues.Add(s.Value.ToString());
            }
            _databaseKeyValue = childValues;
        }
        public bool IsAdmin(DatabaseReference databaseReference, string eNumber)
        {
            if (databaseReference == null)
            {

            }
            databaseReference.Child("users").Child(eNumber).AddValueEventListener(this);
            return true;
        }

    }
    

}

