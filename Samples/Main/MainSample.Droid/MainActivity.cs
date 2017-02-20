
using Android.App;
using Android.OS;
using Android.Support.V7.App;
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

            SupportFragmentManager.BeginTransaction()
                .Add(Resource.Id.frameLayout, new ContentItemFragment(), null)
                .Commit();
		}
	}
}


