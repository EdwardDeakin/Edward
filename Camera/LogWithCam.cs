
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
using Android.Provider;
using Java.Util;
using Android.Text; 

namespace Camera
{


	[Activity(Label = "Alcohol")]
	public class LogWithCam : ListActivity
	{
		int _calId;
		string text1;
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.EventList);

			_calId = Intent.GetIntExtra("calId", -1);

			ListEvents();

			InitAddEvent();

			Button button = FindViewById<Button>(Resource.Id.addSampleEvent);

			button.Click += delegate
			{
				StartActivity(typeof(Camera));
			};
		}

		void ListEvents()
		{
			var eventsUri = CalendarContract.Events.ContentUri;

			string[] eventsProjection = {
				CalendarContract.Events.InterfaceConsts.Id,
				CalendarContract.Events.InterfaceConsts.Title,
				CalendarContract.Events.InterfaceConsts.Dtstart
			 };

			var cursor = ManagedQuery(eventsUri, eventsProjection,
			 String.Format("calendar_id={0}", _calId), null, "dtstart ASC");

			string[] sourceColumns = {
				CalendarContract.Events.InterfaceConsts.Title,
				CalendarContract.Events.InterfaceConsts.Dtstart
			};

			int[] targetResources = { Resource.Id.eventTitle, Resource.Id.eventStartDate };

			var adapter = new SimpleCursorAdapter(this, Resource.Layout.EventListItem,
			 cursor, sourceColumns, targetResources);

			adapter.ViewBinder = new ViewBinder();

			ListAdapter = adapter;

		
		}

		void InitAddEvent()
		{

			EditText edittext = FindViewById<EditText>(Resource.Id.editText);
			edittext.KeyPress += (object sender, View.KeyEventArgs e) =>
			{
				e.Handled = false;
				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
				{
					Toast.MakeText(this, edittext.Text, ToastLength.Short).Show();
					e.Handled = true;
				}
			};

			EditText edittext2 = FindViewById<EditText>(Resource.Id.editText12);
			edittext.KeyPress += (object sender, View.KeyEventArgs e) =>
			{
				e.Handled = false;
				if (e.Event.Action == KeyEventActions.Down && e.KeyCode == Keycode.Enter)
				{
					Toast.MakeText(this, edittext.Text, ToastLength.Short).Show();
					e.Handled = true;
				}
			};



			var addSampleEvent = FindViewById<Button>(Resource.Id.addSampleEvent);

			addSampleEvent.Click += (sender, e) =>
			{
				DateTime today = DateTime.Now;

				var text13 = FindViewById<EditText>(Resource.Id.editText).Text;
				var text14 = FindViewById<EditText>(Resource.Id.editText12).Text;

				ContentValues eventValues = new ContentValues();
				eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, _calId);
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, text13);
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, text14);
				eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(today.Year, today.Month, today.Day, 10, 0));


				eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(today.Year, today.Month, today.Day, 11, 0));


				eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
				eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

				var uri = ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
				Console.WriteLine("Uri for new event: {0}", uri);
			};


		}

		class ViewBinder : Java.Lang.Object, SimpleCursorAdapter.IViewBinder
		{
			public bool SetViewValue(View view, Android.Database.ICursor cursor, int columnIndex)
			{
				if (columnIndex == 2)
				{
					long ms = cursor.GetLong(columnIndex);

					DateTime date =
						new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(ms).ToLocalTime();

					TextView textView = (TextView)view;
					textView.Text = date.ToLongDateString();

					return true;
				}
				return false;
			}
		}

		long GetDateTimeMS(int yr, int month, int day, int hr, int min)
		{
			Calendar c = Calendar.GetInstance(Java.Util.TimeZone.Default);
			DateTime today = DateTime.Now;

			c.Set(Calendar.DayOfMonth, today.Day);
			c.Set(Calendar.HourOfDay, today.Hour);
			c.Set(Calendar.Minute, min);
			c.Set(Calendar.Month, today.Month);
			c.Set(Calendar.Year, today.Year);

			return c.TimeInMillis;


		}


	} 
} 



