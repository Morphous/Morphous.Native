using System;
using Foundation;
using Morphous.Native.iOS;
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
            var contentViewController = MphIOS.StackedContentItemViewController(16);
            ShowViewController(contentViewController, this);
        }

        partial void TermButtonClicked(NSObject sender)
        {
            var contentViewController = MphIOS.TableContentItemViewController(13);
            ShowViewController(contentViewController, this);
        }

        partial void TaxonomyButtonClicked(NSObject sender)
        {
        	var contentViewController = MphIOS.TableContentItemViewController(12);
        	ShowViewController(contentViewController, this);
        }
    }
}