using Foundation;
using System;
using UIKit;

namespace Morphous.Native.iOS.UI
{
    public partial class ContentItemView : UIScrollView
    {
        public ContentItemView (IntPtr handle) : base (handle)
        {
        }

        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            var test = Header;
        }
    }
}