using System;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS.Factories
{
    public interface IViewFactory
    {
        UIView CreateContentItemView(IContentItem contentItem);
    }
}
