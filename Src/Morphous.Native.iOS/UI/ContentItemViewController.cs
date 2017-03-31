using Foundation;
using System;
using UIKit;

namespace Morphous.Native.iOS.UI
{
    public partial class ContentItemViewController : UIViewController
    {
        public ContentItemViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}