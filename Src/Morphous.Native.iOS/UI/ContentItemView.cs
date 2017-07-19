using Foundation;
using System;
using UIKit;
using Morphous.Native.Models;
using ObjCRuntime;

namespace Morphous.Native.iOS.UI
{
    public partial class ContentItemView : UIView
    {
        //TODO keep track of an dispose elements

        public ContentItemView (IntPtr handle) : base (handle)
        {
        }

        public DisplayContext DisplayContext { get; set; }

        private IContentItem _contentItem;
        public IContentItem ContentItem
        {
            get { return _contentItem; }
            set
            {
                _contentItem = value;
                Update();
            }
        }

        void Update()
        {
            foreach (var zone in _contentItem.Zones)
            {
                var zoneProp = this.GetType().GetProperty(zone.Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var zoneView = zoneProp?.GetValue(this, null) as UIView;

                if (zoneView != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementView = DisplayContext.ViewFactory.CreateElementView(element);

                        if (elementView != null)
                        {
                            if (zoneView is UIStackView)
                            {
                                ((UIStackView)zoneView).AddArrangedSubview(elementView);
                            }
                            else
                            {
                                zoneView.AddSubview(elementView);
                            }
                        }
                    }
                }
            }
        }
   }
}