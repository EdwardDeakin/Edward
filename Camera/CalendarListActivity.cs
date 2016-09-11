using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Java.Util;

namespace Camera
{
    [Activity (Label = "Choose Calender")]
    public class CalendarListActivity : ListActivity


    {
		int _numpass;

	
        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.CalendarList);

		_numpass = Intent.GetIntExtra("numpass1", 0);
           
            // List Calendars
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            
            string[] calendarsProjection = {
               CalendarContract.Calendars.InterfaceConsts.Id,
               CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
               CalendarContract.Calendars.InterfaceConsts.AccountName
            };
            
            var cursor = ManagedQuery (calendarsUri, calendarsProjection, null, null, null);
            
            string[] sourceColumns = {CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName, 
                CalendarContract.Calendars.InterfaceConsts.AccountName};
            
            int[] targetResources = {Resource.Id.calDisplayName, Resource.Id.calAccountName};       
            
            SimpleCursorAdapter adapter = new SimpleCursorAdapter (this, Resource.Layout.CalListItem, 
                cursor, sourceColumns, targetResources);
            
            ListAdapter = adapter;
            

            ListView.ItemClick += (sender, e) => { 
               


				 if (_numpass == 2)
				{
					int i = (e as Android.Widget.AdapterView.ItemClickEventArgs).Position;

					cursor.MoveToPosition(i);
					int calId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));

					var showEvents = new Intent(this, typeof(LogWithCam));
					showEvents.PutExtra("calId", calId);
					StartActivity(showEvents);
				}

				else if (_numpass == 3)
				{
					int i = (e as Android.Widget.AdapterView.ItemClickEventArgs).Position;

					cursor.MoveToPosition(i);
					int calId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));

					var showEvents = new Intent(this, typeof(Log));
					showEvents.PutExtra("calId", calId);
					StartActivity(showEvents);
				}


				//else if (_numpass == 7)
				//{
				//int i = (e as Android.Widget.AdapterView.ItemClickEventArgs).Position;

				//	cursor.MoveToPosition(i);
				//	int calId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));

				//	var showEvents = new Intent(this, typeof(EventListActivity6));
				//	showEvents.PutExtra("calId", calId);
				//	StartActivity(showEvents);
				//}
			};
        }
    }
}