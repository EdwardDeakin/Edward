
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

namespace Camera
{
	[Activity(Label = "RecovHomePage", MainLauncher = true)]
	public class RecovHomePage : Activity
	{
		Button bu1;
		Button bu2;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.RecovHome);
			bu1 = FindViewById<Button>(Resource.Id.b1);

			bu2 = FindViewById<Button>(Resource.Id.b2);
			// Create your application here
			if (bu1 != null)
			{
				bu1.Click += (sender, e) =>
				{
					int numpass = 2;

					var showEvents1 = new Intent(this, typeof(CalendarListActivity));
					showEvents1.PutExtra("numpass1", numpass);
					StartActivity(showEvents1); ;
				};
			}


			if (bu2 != null)
			{

				bu2.Click += (sender, e) =>

				{
					int numpass2 = 3;
					var showEvents2 = new Intent(this, typeof(CalendarListActivity));
					showEvents2.PutExtra("numpass1", numpass2);
					StartActivity(showEvents2);
				};
			}

		}
	}
}

