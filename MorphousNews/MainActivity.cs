using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Morphous.Native.Droid.UI;
using Morphous.Native.Droid;
using Morphous.Native.ViewModels;
using Android.Support.V4.App;
using Morphous.Native.Models;
using System.Collections.Generic;
using Java.Lang;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Messages;

namespace MorphousNews
{
    [Activity(Label = "MorphousNews", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Android.Support.V7.App.AppCompatActivity
    {
        private IMessenger _messenger;
        private IMessenger Messenger
        {
            get { return _messenger ?? (_messenger = GalaSoft.MvvmLight.Messaging.Messenger.Default); }
            set { _messenger = value; }
        }

        private IContentItemViewModel _viewModel;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.swiperefresh).Refresh += MainActivity_Refresh;

            _viewModel = ContentItemViewModel.Create(14);
            _viewModel.PropertyChanged += _viewModel_PropertyChanged;
        }

        private void MainActivity_Refresh(object sender, EventArgs e)
        {
            _viewModel.Refresh.Execute(null);
        }

        private void _viewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ContentItem" && _viewModel.ContentItem != null)
            {
                FindViewById<Android.Support.V4.Widget.SwipeRefreshLayout>(Resource.Id.swiperefresh).Refreshing = false;

                var terms = _viewModel.ContentItem.As<ITaxonomyPart>().Terms;

                var viewPager = FindViewById<Android.Support.V4.View.ViewPager>(Resource.Id.viewPager);
                viewPager.Adapter = new TermsADapter(terms, SupportFragmentManager);

                var tabs = FindViewById<Android.Support.Design.Widget.TabLayout>(Resource.Id.tabLayout);
                tabs.SetupWithViewPager(viewPager);
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

        class TermsADapter : FragmentStatePagerAdapter
        {
            private IList<ITaxonomyItem> _terms;

            public TermsADapter(IList<ITaxonomyItem> terms, Android.Support.V4.App.FragmentManager fm) : base(fm)
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

