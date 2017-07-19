using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Factories;
using Morphous.Native.Droid.Messages;
using Morphous.Native.Droid.UI;
using Morphous.Native.Droid.UI.Elements;
using Morphous.Native.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Morphous.Native.Droid.Bindings
{
    public static class ContentItemBindingExtensions
    {
        public static ContentItemBinding SetContentBinding(
            this Activity source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
        {
            return new ContentItemBinding(source, source, sourcePropertyExpression, targetPropertyExpression);
        }

        public static ContentItemBinding SetContentBinding(
            this Android.Support.V4.App.Fragment source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
        {
            return new ContentItemBinding(source, source.Activity, sourcePropertyExpression, targetPropertyExpression);
        }
    }

    public class ContentItemBinding : Binding<IContentItem, View>
    {
        private readonly Activity _activity;
        private readonly Func<IContentItem> _sourcePropertyFunc;
        private readonly Func<View> _targetPropertyFunc;

        private ContentItemViewHolder _contentItemViewHolder;

        public ContentItemBinding(
            object source,
            Activity activity,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
            : base(
                source,
                sourcePropertyExpression,
                null,
                null,
                BindingMode.OneWay,
                null,
                null)
        {
            _activity = activity;
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

            _contentItemViewHolder?.Dispose();

            var contentItemContainer = view.FindViewById<ViewGroup>(Resource.Id.contentItem_container);
            if (contentItemContainer == null)
                throw new ArgumentException("ContentItemBinding requires a view with a ViewGroup with id contentItem_container");
            
            var displayContext = new DisplayContext();
            displayContext.Activity = _activity;
            displayContext.Inflater = _activity.LayoutInflater;
            displayContext.Messenger = Messenger.Default;
            displayContext.RootContainer = contentItemContainer;
            displayContext.RootContentItem = contentItem;
            displayContext.ViewHolderFactory = new DefaultViewHolderFactory(displayContext);

            displayContext.Messenger.Send(new ContentItemDisplayingMessage(displayContext));

            _contentItemViewHolder = displayContext.RootContentItemViewHolder();            
            contentItemContainer.AddView(_contentItemViewHolder.View);
        }

        public override void Detach()
        {
            base.Detach();
            _contentItemViewHolder?.Dispose();
        }
    }
}