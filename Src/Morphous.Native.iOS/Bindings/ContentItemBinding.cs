using System;
using System.Linq.Expressions;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.iOS.Factories;
using Morphous.Native.iOS.UI;
using Morphous.Native.Models;
using UIKit;

namespace Morphous.Native.iOS.Bindings
{
    public static class ContentItemBindingExtensions
    {
        public static ContentItemBinding SetContentBinding(
            this UIViewController source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<UIView>> targetPropertyExpression)
        {
            return new ContentItemBinding(source, source, sourcePropertyExpression, targetPropertyExpression);
        }
    }

    public class ContentItemBinding : Binding<IContentItem, UIView>
    {
        private readonly UIViewController _viewController;
        private readonly Func<IContentItem> _sourcePropertyFunc;
        private readonly Func<UIView> _targetPropertyFunc;

        private UIView _contentItemView;

        public ContentItemBinding(
            object source,
            UIViewController viewController,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<UIView>> targetPropertyExpression)
            : base(
                source,
                sourcePropertyExpression,
                null,
                null,
                BindingMode.OneWay,
                null,
                null)
        {
            _viewController = viewController;
            _sourcePropertyFunc = sourcePropertyExpression.Compile();
            _targetPropertyFunc = targetPropertyExpression.Compile();
            this.WhenSourceChanges(Update);
        }

        private void Update()
        {
            var contentItem = _sourcePropertyFunc();
            var view = _targetPropertyFunc();

            if (contentItem == null || view == null)
                return;

            _contentItemView?.Dispose();

            var displayContext = new DisplayContext();
            displayContext.ViewController = _viewController;
            displayContext.Messenger = Messenger.Default;
            displayContext.RootView = view;
            displayContext.RootContentItem = contentItem;
            displayContext.ViewFactory = new DefaultViewFactory(displayContext);

            //displayContext.Messenger.Send(new ContentItemDisplayingMessage(displayContext));

            _contentItemView = displayContext.RootContentItemView();
            displayContext.RootView.AddSubview(_contentItemView);
            displayContext.RootView.AddConstraints(ContentConstraints(_contentItemView, displayContext.RootView));
        }

        public override void Detach()
        {
            base.Detach();
            _contentItemView?.Dispose();
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
