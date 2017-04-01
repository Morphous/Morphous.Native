using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Morphous.Native.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.iOS.Bindings;

namespace Morphous.Native.iOS.UI
{
    public partial class ContentItemViewController : UIViewController
    {
        private IContentItemViewModel ViewModel { get; set; }

        private UIView _contentItemView;

        public ContentItemViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel = ContentItemViewModel.Create(16);

            this.SetContentBinding(() => ViewModel.ContentItem, () => this.ScrollView);
        }
    }
}