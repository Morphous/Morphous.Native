using System;
using Morphous.Native.iOS;
using UIKit;

namespace News.iOS
{
    public partial class ViewController : TableContentItemViewController
    {

        public override int ContentItemId
        {
            get { return 12; }
            set { }
        }

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }
    }
}
