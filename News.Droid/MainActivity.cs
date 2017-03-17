using Android.App;
using Android.Widget;
using Android.OS;
using Morphous.Native.Droid.UI;

namespace News.Droid
{
    [Activity(Label = "News.Droid", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : ContentItemActivity
    {
        public override int ContentItemId => 12;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
        }
    }
}

