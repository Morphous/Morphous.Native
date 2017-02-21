
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
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

            var gcButton = FindViewById<Button>(Resource.Id.button_gc);
            gcButton.Click += GcButton_Click;
		}

        private void Button_Click(object sender, System.EventArgs e)
        {
            StartActivity(typeof(TestContentItemActivity));
        }

        private void GcButton_Click(object sender, System.EventArgs e)
        {
            var mainApplication = (MainApplication)Application;
            mainApplication.GarbageCollect();
        }
    }
}


