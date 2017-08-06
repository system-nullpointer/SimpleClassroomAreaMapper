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

namespace SCAM
{
    [Activity(Label = "Search for Friend")]
    public class AddFriend : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AddFriendLayout);


            Android.Widget.Button searchBtn = FindViewById<Android.Widget.Button>(Resource.Id.searchBtn);

            searchBtn.Click += (sender, e) =>
            {
                EditText firstBox = FindViewById<EditText>(Resource.Id.firstBox);

                AlertDialog alert = new AlertDialog.Builder(this).Create();

                Student demo = Helper.User;

                EditText lastBox = FindViewById<EditText>(Resource.Id.lastBox);

                List<Student> available = Helper.available;
                for (int i = 0; i < available.Count; i++)
                {
                    if (string.Equals(available[i].FirstName, firstBox.Text.Trim()) && available[i].LastName.Equals(lastBox.Text.Trim()))
                    {
                        if (Student.FindStudentByID(available[i].ID, demo.Friends) != null)
                        {
                            new AlertDialog.Builder(this)
                            .SetTitle("Error!")
                            .SetMessage("This person is already in your friends list!")
                            .SetPositiveButton("OK", delegate { this.Finish(); })
                            .Show();
                        }
                        else
                        {
                            demo.AddFriend(available[i]);

                            new AlertDialog.Builder(this)
                            .SetTitle("Success!")
                            .SetMessage("This person has been added to your friends list!")
                            .SetPositiveButton("OK", delegate { this.Finish(); })
                            .Show();
                            break;
                        }
                    }
                    else if (i == available.Count - 1)
                    {
                        new AlertDialog.Builder(this)
                        .SetTitle("Error")
                        .SetMessage("User with that name was not found. Could not add to friends list.")
                        .SetPositiveButton("OK", delegate { this.Finish(); })
                        .Show();
                    }
                }

                //Finish();
            };
        }
    }
}