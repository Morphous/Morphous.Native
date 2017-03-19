using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Morphous.Native.ViewModels;
using Morphous.Native.Models;
using Android.Support.V4.App;
using System.Collections.Generic;
using System;
using Java.Lang;
using Morphous.Native.Droid.UI;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Morphous.Native.Droid.Messages;
using GalaSoft.MvvmLight.Messaging;
using Android.Support.V7.Widget;
using Android.Content;
using Morphous.Native.Droid;

namespace News.Droid
{
    [Activity(Label = "News", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        private IContentItemViewModel ViewModel { get; set; }

        private IMessenger _messenger;
        private IMessenger Messenger
        {
            get { return _messenger ?? (_messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default); }
            set { _messenger = value; }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            
            ViewModel = ContentItemViewModel.Create(12);
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IContentItemViewModel.ContentItem))
            {
                var taxonomyPart = ViewModel.ContentItem.As<TaxonomyPart>();

                var viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
                viewPager.Adapter = new TermsAdapter(SupportFragmentManager, taxonomyPart.Terms);

                var tabLayout = FindViewById<TabLayout>(Resource.Id.tabLayout);
                tabLayout.SetupWithViewPager(viewPager);
            }
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
            var intent = new Intent(this, typeof(ContentItemActivity));
            intent.PutExtra(MphExtras.ContentItemId, obj.ContentItem.Id);
            StartActivity(intent);
        }

        private class TermsAdapter : FragmentStatePagerAdapter
        {
            private IList<ITaxonomyItem> _terms;

            public TermsAdapter(Android.Support.V4.App.FragmentManager fragmentManager, IList<ITaxonomyItem> terms) : base(fragmentManager)
            {
                _terms = terms;
            }

            public override int Count => _terms.Count;

            public override Android.Support.V4.App.Fragment GetItem(int position)
            {
                return ContentItemFragment.Create(_terms[position].Id);
            }

            public override ICharSequence GetPageTitleFormatted(int position)
            {
                return new Java.Lang.String(_terms[position].Title);
            }
        }
    }
}

