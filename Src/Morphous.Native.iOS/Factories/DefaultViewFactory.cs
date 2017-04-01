using System;
using Foundation;
using Morphous.Native.iOS.UI;
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
            var contentItemView = Runtime.GetNSObject<ContentItemView>(arr.ValueAt(0));

            contentItemView.TranslatesAutoresizingMaskIntoConstraints = false;
            contentItemView.DisplayContext = _displayContext;
            contentItemView.ContentItem = contentItem;

            return contentItemView;
        }

        public UIView CreateElementView(IContentElement element)
        {
            foreach (var alternate in element.Alternates)
            {
                var path = NSBundle.MainBundle.PathForResource(alternate, "nib");

                if (path != null)
                {
                    var arr = NSBundle.MainBundle.LoadNib(element.Type, null, null);
                    var view = Runtime.GetNSObject<UIView>(arr.ValueAt(0));

                    view.TranslatesAutoresizingMaskIntoConstraints = false;

                    if (view is ElementView)
                    {
                        var elementView = (ElementView)view;
                        elementView.DisplayContext = _displayContext;
                        elementView.SetElement(element);
                    }

                    return view;
                }
            }

            return null;
        }
    }
}
