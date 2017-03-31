using Foundation;
using System;
using UIKit;
using ObjCRuntime;
using Morphous.Native.ViewModels;
using GalaSoft.MvvmLight.Helpers;

namespace Morphous.Native.iOS.UI
{
    public partial class ContentItemViewController : UIViewController
    {
        private IContentItemViewModel ViewModel { get; set; }

        private UIView _contentItemView;

        public ContentItemViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            ViewModel = ContentItemViewModel.Create(16);

            var arr = NSBundle.MainBundle.LoadNib("ContentItem_", null, null);
            _contentItemView = Runtime.GetNSObject<ContentItemView>(arr.ValueAt(0));

            _contentItemView.TranslatesAutoresizingMaskIntoConstraints = false;

            View.AddSubview(_contentItemView);
            View.AddConstraints(ContentConstraints(_contentItemView));

            this.AutomaticallyAdjustsScrollViewInsets = false;
            this.AutomaticallyAdjustsScrollViewInsets = true;

            this.SetBinding(() => ViewModel.ContentItem).WhenSourceChanges(Update);
        }

        private void Update()
        {
            if (ViewModel.ContentItem == null)
                return;

            foreach (var zone in ViewModel.ContentItem.Zones)
            {
                var zoneProp = _contentItemView.GetType().GetProperty(zone.Name, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var zoneView = zoneProp?.GetValue(_contentItemView, null) as UIView;

                if (zoneView != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var arr = NSBundle.MainBundle.LoadNib("TitlePart", null, null);
                        var elementView = Runtime.GetNSObject<UIView>(arr.ValueAt(0));

                        elementView.TranslatesAutoresizingMaskIntoConstraints = false;

                        zoneView.AddSubview(elementView);
                    }
                }
            }
        }

        private NSLayoutConstraint[] ContentConstraints(UIView contentItemView)
        {
            return new NSLayoutConstraint[] {
                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Left,
                    NSLayoutRelation.Equal,
                    View,
                    NSLayoutAttribute.Left,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Right,
                    NSLayoutRelation.Equal,
                    View,
                    NSLayoutAttribute.Right,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Top,
                    NSLayoutRelation.Equal,
                    View,
                    NSLayoutAttribute.Top,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Bottom,
                    NSLayoutRelation.Equal,
                    View,
                    NSLayoutAttribute.Bottom,
                    1, 0)
            };
        }
    }
}