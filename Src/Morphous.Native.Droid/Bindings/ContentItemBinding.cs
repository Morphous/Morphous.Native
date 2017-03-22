using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Factories;
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
            this object source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression)
        {
            return new ContentItemBinding(source, sourcePropertyExpression, targetPropertyExpression);
        }
    }

    public class ContentItemBinding : Binding<IContentItem, View>
    {
        private readonly Func<IContentItem> _sourcePropertyFunc;
        private readonly Func<View> _targetPropertyFunc;
        private readonly List<ElementViewHolder> _elementViewHolders = new List<ElementViewHolder>();

        private ContentItemViewHolder _contentItemViewHolder;

        public ContentItemBinding(
            object source,
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
            displayContext.Context = view.Context;
            displayContext.Inflater = LayoutInflater.From(view.Context);
            displayContext.Messenger = Messenger.Default;
            displayContext.RootContainer = contentItemContainer;
            displayContext.RootContentItem = contentItem;
            displayContext.ViewHolderFactory = new DefaultViewHolderFactory(displayContext);

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