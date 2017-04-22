using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Morphous.Native.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.iOS.Bindings;

namespace Morphous.Native.iOS.UI
{
    public partial class NormalContentItemViewController : UIViewController
    {
        private IContentItemViewModel ViewModel { get; set; }

        private Binding _contentItemBinding;

        public virtual int ContentItemId { get; set; }

        public NormalContentItemViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel = ContentItemViewModel.Create(ContentItemId);

            _contentItemBinding = this.SetContentBinding(() => ViewModel.ContentItem, () => this.ScrollView);
        }
    }
}