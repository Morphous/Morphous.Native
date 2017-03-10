using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Android.Content;
using Morphous.Native.Droid.UI;

namespace MainSample.Droid
{
	[Activity (Label = "MainSample.Droid", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : AppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            var button = FindViewById<Button>(Resource.Id.button);
            button.Click += Button_Click;

            var termButton = FindViewById<Button>(Resource.Id.button_term);
            termButton.Click += TermButtonClick;

            var gcButton = FindViewById<Button>(Resource.Id.button_gc);
            gcButton.Click += GcButton_Click;
		}

        private void Button_Click(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(ContentItemActivity));
            intent.PutExtra(MphExtras.ContentItemId, 12);
            StartActivity(intent);
        }

        private void TermButtonClick(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(ContentItemActivity));
            intent.PutExtra(MphExtras.ContentItemId, 14);
            StartActivity(intent);
        }

        private void GcButton_Click(object sender, System.EventArgs e)
        {
            var mainApplication = (MainApplication)Application;
            mainApplication.GarbageCollect();
        }
    }
}


