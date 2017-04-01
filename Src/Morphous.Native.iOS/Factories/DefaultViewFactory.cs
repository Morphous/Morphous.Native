using System;
using Foundation;
using Morphous.Native.Models;
using ObjCRuntime;
using UIKit;

namespace Morphous.Native.iOS.Factories
{
    public class DefaultViewFactory : IViewFactory
    {
        private readonly DisplayContext _displayContext;

        public DefaultViewFactory(DisplayContext displayContext)
        {
            _displayContext = displayContext;
        }

        public UIView CreateContentItemView(IContentItem contentItem)
        {
            var arr = NSBundle.MainBundle.LoadNib("ContentItem_", null, null);
            var contentItemView = Runtime.GetNSObject<UIView>(arr.ValueAt(0));

            contentItemView.TranslatesAutoresizingMaskIntoConstraints = false;

            return contentItemView;
        }
    }
}
