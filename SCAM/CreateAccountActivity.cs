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
using Firebase.Auth;
using Android.Gms.Tasks;
using Android.Util;

namespace SCAM
{
    [Activity(Label = "Activity1")]
    public class CreateAccountActivity : Activity, IOnCompleteListener
    {
        FirebaseAuth auth;
        public string _email;
        public string _password;
        public string _passwordToBeVerified;
        
        
        

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CreateAccountLayout);

            Button submitButton = FindViewById<Button>(Resource.Id.submit);
            EditText email = FindViewById<EditText>(Resource.Id.emailAddress);
            EditText password = FindViewById<EditText>(Resource.Id.password);
            EditText passwordToBeVerified = FindViewById<EditText>(Resource.Id.passwordToBeVerified);
            auth = FirebaseAuth.Instance;
            
            submitButton.Click += (sender,e) =>
            {
                _email = email.Text;
                _password = password.Text;
                _passwordToBeVerified = passwordToBeVerified.Text;
                if (!_password.Equals(_passwordToBeVerified))
                {
                    Toast.MakeText(this, "Passwords do not match ", ToastLength.Short).Show();
                    return;
                }
                auth.CreateUserWithEmailAndPassword(email.Text, password.Text).AddOnCompleteListener(this);

            };
            //auth.CreateUserWithEmailAndPassword

            // Create your application here
        }
        

        public void OnComplete(Task task)
        {
            try
            {
                if (task.IsSuccessful)
                {
                    //isSignedIn = true;
                    Toast.MakeText(this, "Account was created successfully", ToastLength.Short).Show();
                    Finish();
                }
                else
                {
                    //isSignedIn = false;
                    Toast.MakeText(this, "Account was not created successfully", ToastLength.Short).Show();
                }

            }
            catch (FirebaseAuthWeakPasswordException e)
            {
                Toast.MakeText(this, $"{e.Message}", ToastLength.Short).Show();

                throw;
            }
           
        }
    }
}