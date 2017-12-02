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
using Android.Support.V4.App;
using Android;

namespace SCAM
{
    
    [Activity(Label = "SCAM", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.AppCompat.Light", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity :  Activity
    {
        
        private FirebaseClient firebase;
        private DatabaseReference _database;
        private List<string> _databaseKeyValue;
        public int MyResultCode = 1;
        private bool isSignedIn = false;
        public static volatile string _currentEmail;

        //This dictionary will hold the email addresses of current users and determine if they are an admin or not
        private List<string> AdminList = new List<string>();

        private Button campusMapButton;
        private Button friendsButton;
        //private Button messagesButton;
        private Button adminButton;
        private Button viewScheduleButton;

        protected override void OnCreate(Bundle bundle)
        {
            try
            {
                base.OnCreate(bundle);
                //updated this to SignIn instead of Main
                SetContentView(Resource.Layout.Main);


                //If the permissions are not set fir the map, then prompt the user for the proper permissions
                if (CheckCallingOrSelfPermission(Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 1);
                }

                if (CheckCallingOrSelfPermission(Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, 1);
                }

                List<Student> availableStudents = Helper.createStudents();

                campusMapButton = FindViewById<Button>(Resource.Id.campusMap);
                friendsButton = FindViewById<Button>(Resource.Id.friends);
                //messagesButton = FindViewById<Button>(Resource.Id.messages);
                adminButton = FindViewById<Button>(Resource.Id.admin);
                viewScheduleButton = FindViewById<Button>(Resource.Id.viewSchedule);

                //Disable the admin button initially
                adminButton.Visibility = Android.Views.ViewStates.Invisible;
                adminButton.Clickable = true;

                //This will be used for demonstration purposes. We can fix this later
                AdminList.Add("andrewb2495@gmail.com");

                //If the current user is an admin, then display the admin button
                if (AdminList.Contains(FirebaseAuth.Instance.CurrentUser?.Email) && adminButton != null && _currentEmail != null)
                {
                    adminButton.Visibility = Android.Views.ViewStates.Visible;
                    adminButton.Clickable = true;
                }

                if (FirebaseAuth.Instance.CurrentUser == null)
                {
                    StartActivity(new Android.Content.Intent(this, typeof(SignIn)));
                }

                if (SignIn.isSignedIn == false)
                {
                    firebase = new FirebaseClient(GetString(Resource.String.firebase_database_url));
                }

                FirebaseApp.InitializeApp(this);
                _database = FirebaseDatabase.Instance.Reference;


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

                /*
                messagesButton.Click += (sender, e) =>
                {
                    var intent = new Intent(this, typeof(MessageActivity));
                    StartActivity(intent);
                };
                */
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
            }
            catch (Exception ex)
            {

                Toast.MakeText(this, ex.Message, ToastLength.Long).Show();
            }
           
        }

        protected override void OnResume()
        {
            base.OnResume();

            //If the current user is an admin, then display the admin button
            if (AdminList.Contains(FirebaseAuth.Instance.CurrentUser?.Email) && adminButton != null && _currentEmail != null)
            {
                adminButton.Visibility = Android.Views.ViewStates.Visible;
                adminButton.Clickable = true;
            }
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
        /*
        public void OnResume()
        {
            base.OnResume();
            SetContentView(Resource.Layout.Main);
        }
        */
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

        
    }
    

}

