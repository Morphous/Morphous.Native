using System;
using Android.OS;
using Android.Support.V4.App;
using Android.Views;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.Models;
using Morphous.Native.ViewModels;
using Android.Widget;
using Morphous.Native.Droid.Bindings;

namespace Morphous.Native.Droid.UI
{
    public class ContentItemFragment : Fragment
    {
        private ProgressBar _loadingSpinner;
        public ProgressBar LoadingSpinner => _loadingSpinner ?? (_loadingSpinner = View.FindViewById<ProgressBar>(Resource.Id.progressBar));




        private IContentItemViewModel ViewModel { get; set; }

        private Binding _contentItemBinding;
        private Binding _loadingSpinnerBinding;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ViewModel = ContentItemViewModel.Create();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.ContentItem, container, false);
        }

        public override void OnViewCreated(View view, Bundle savedInstanceState)
        {
            base.OnViewCreated(view, savedInstanceState);

            _contentItemBinding = this.SetContentBinding(() => ViewModel.ContentItem, () => view);

            _loadingSpinnerBinding = this
                .SetBinding(() => ViewModel.Loading, () => LoadingSpinner.Visibility)
                .ConvertSourceToTarget(BoolToVisibility);
        }

        private ViewStates BoolToVisibility(bool arg)
        {
            return arg ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}