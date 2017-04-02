using System;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.iOS.Factories;
using Morphous.Native.iOS.UI;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS
{
    public class DisplayContext
    {
        public UIViewController ViewController { get; set; }
        public UIView RootView { get; set; }
        public IMessenger Messenger { get; set; }
        public IViewFactory ViewFactory { get; set; }
        public IContentItem RootContentItem { get; set; }

        public UIView RootContentItemView()
        {
            return ViewFactory.CreateContentItemView(RootContentItem);
        }
    }
}
