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
    [Activity(Label = "SCAM Sign In",Theme = "@style/Theme.AppCompat.Light")]
    public class SignIn : AppCompatActivity, IOnCompleteListener
    {
        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SignIn);

            auth = FirebaseAuth.Instance;

            var edtEmail = FindViewById<EditText>(Resource.Id.edtEmail);
            var edtPassword = FindViewById<EditText>(Resource.Id.edtPassword);
            var btnRegister = FindViewById<Button>(Resource.Id.btnRegister);

            var user = auth.CurrentUser;

            btnRegister.Click += delegate 
            {
                auth.SignInWithEmailAndPassword(edtEmail.Text, edtPassword.Text)
                .AddOnCompleteListener(this);
            };
         
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                Toast.MakeText(this, "Welcome " + FirebaseAuth.Instance.CurrentUser.Email, ToastLength.Short).Show();
                Toast.MakeText(this, "SignIn successful!", ToastLength.Short).Show();
                Finish();
            }
            else
            {
                Toast.MakeText(this, "SignIn failed!", ToastLength.Short).Show();
            }
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            var intent = new Intent(this, typeof(SignIn));
            StartActivity(intent);
            Finish();
        }
    }
}