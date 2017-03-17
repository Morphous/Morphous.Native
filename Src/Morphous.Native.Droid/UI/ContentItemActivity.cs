using System;

using Android.App;
using Android.OS;
using Android.Support.V7.App;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Events;
using Android.Content;

namespace Morphous.Native.Droid.UI
{
    [Activity(Label = "Content Item")]
    public class ContentItemActivity : AppCompatActivity
    {
        private IMessenger _messenger;
        private IMessenger Messenger
        {
            get { return _messenger ?? (_messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default); }
            set { _messenger = value; }
        }

        public virtual int ContentItemId
        {
            get
            {
                var contentItemId = Intent.GetIntExtra(MphExtras.ContentItemId, -1);

                if (contentItemId == -1)
                    throw new ArgumentException("ContentItemActivity must be started with an int extra for the content item id.");
                
                return contentItemId;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_content_item);

            if (bundle != null)
                return;

            SupportFragmentManager.BeginTransaction()
                .Add(Resource.Id.frameLayout, ContentItemFragment.Create(ContentItemId), null)
                .Commit();
        }

        protected override void OnResume()
        {
            base.OnResume();
            Messenger.Register<ContentItemSelectedMessage>(this, ContentItemSelected);
        }

        protected override void OnPause()
        {
            base.OnPause();
            Messenger.Unregister<ContentItemSelectedMessage>(this);
        }

        private void ContentItemSelected(ContentItemSelectedMessage obj)
        {
            var id = obj.ContentItem.Id;

            if (id.HasValue)
            {
                var intent = new Intent(this, typeof(ContentItemActivity));
                intent.PutExtra(MphExtras.ContentItemId, id.Value);
                StartActivity(intent);
            }
        }
    }
}