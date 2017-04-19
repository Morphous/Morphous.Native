using System;
using Foundation;
using Morphous.Native.iOS.UI;
using UIKit;

namespace MainSample.iOS
{
    public partial class ViewController : UIViewController
    {
        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        partial void ButtonClicked(NSObject sender)
        {
            var contentStoryboard = UIStoryboard.FromName("Content", NSBundle.MainBundle);
            var contentViewController = (ContentItemViewController)contentStoryboard.InstantiateViewController("ContentItemViewController");

            contentViewController.ContentItemId = 16;

            ShowViewController(contentViewController, this);
        }

        partial void TermButtonClicked(NSObject sender)
        {
            var contentStoryboard = UIStoryboard.FromName("Content", NSBundle.MainBundle);
            var contentViewController = contentStoryboard.InstantiateViewController("TableContentItemViewController");

            ShowViewController(contentViewController, this);
        }
    }
}