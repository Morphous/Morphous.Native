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
            this.SetBinding(() => ViewModel.ContentItem)
                .WhenSourceChanges(ContentItemBinding);
        }

        private void ContentItemBinding()
        {
            if (ViewModel.ContentItem == null)
                return;

            foreach(var zone in ViewModel.ContentItem.Zones)
            {
                var zoneLayout = View.FindViewById<ViewGroup>(Resources.GetIdentifier(zone.Name, "id", Activity.PackageName));

                if (zoneLayout != null)
                {
                    foreach(var element in zone.Elements)
                    {
                        var textView = new TextView(Activity);
                        textView.Text = element.Type;

                        zoneLayout.AddView(textView);
                    }
                }
            }
        }
    }
}