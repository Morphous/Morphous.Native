using System;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS
{
    public class ContentItemCell : UITableViewCell
    {
        public ContentItemCell(DisplayContext displayContext, IContentItem contentItem, string cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            var contentItemView = displayContext.ViewFactory.CreateContentItemView(contentItem);
            contentItemView.TranslatesAutoresizingMaskIntoConstraints = false;

            ContentView.AddSubview(contentItemView);
        //    ContentView.AddConstraints(ContentConstraints(contentItemView, this));
        }

        private NSLayoutConstraint[] ContentConstraints(UIView contentItemView, UIView container)
        {
            return new NSLayoutConstraint[] {
                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Left,
                    NSLayoutRelation.Equal,
                    container,
                    NSLayoutAttribute.Left,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Right,
                    NSLayoutRelation.Equal,
                    container,
                    NSLayoutAttribute.Right,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Top,
                    NSLayoutRelation.Equal,
                    container,
                    NSLayoutAttribute.Top,
                    1, 0),

                NSLayoutConstraint.Create (
                    contentItemView,
                    NSLayoutAttribute.Bottom,
                    NSLayoutRelation.Equal,
                    container,
                    NSLayoutAttribute.Bottom,
                    1, 0)
            };
        }
    }
}
