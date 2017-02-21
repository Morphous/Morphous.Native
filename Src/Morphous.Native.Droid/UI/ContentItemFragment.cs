using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.Models;
using Morphous.Native.ViewModels;
using Android.Widget;

namespace Morphous.Native.Droid.UI
{
    public class ContentItemFragment : Fragment
    {
        public IContentItemViewModel ViewModel { get; private set; }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = ContentItemViewModel.Create();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.fragment_content_item, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            Binding test = new ContentItemBinding(this, Activity, () => ViewModel.ContentItem, () => view);
        }
    }
}