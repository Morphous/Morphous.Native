using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace Morphous.Native.Droid.UI
{
    [Activity(Label = "Content Item")]
    public class ContentItemActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_content_item);

            SupportFragmentManager.BeginTransaction()
                .Add(Resource.Id.frameLayout, new ContentItemFragment(), null)
                .Commit();
        }
    }
}