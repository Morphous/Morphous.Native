using System;

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
            SetContentView(Resource.Layout.activity_content_item);

            var contentItemId = Intent.GetIntExtra(MphExtras.ContentItemId, -1);
            if (contentItemId == -1)
            {
                throw new ArgumentException("ContentItemActivity must be started with an int extra for the content item id.");
            }

            SupportFragmentManager.BeginTransaction()
                .Add(Resource.Id.frameLayout, ContentItemFragment.Create(contentItemId), null)
                .Commit();
        }
    }
}