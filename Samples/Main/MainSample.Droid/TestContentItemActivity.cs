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
using Android.Support.V7.App;
using Morphous.Native.Droid.UI;

namespace MainSample.Droid
{
    [Activity(Label = "Content Item")]
    public class TestContentItemActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_content_item);

            var contentItemFragment = new ContentItemFragment();
            var mainApplication = (MainApplication)Application;
            mainApplication.SetObject(contentItemFragment);

            SupportFragmentManager.BeginTransaction()
                .Add(Resource.Id.frameLayout, contentItemFragment, null)
                .Commit();
        }
    }
}