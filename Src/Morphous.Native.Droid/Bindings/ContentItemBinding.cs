using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Events;
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
        private readonly IElementViewHolderFactory _elementViewHolderFactory;

        private ContentItemViewHolder _contentItemViewHolder;

        public ContentItemBinding(
            object source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression,
            IElementViewHolderFactory elementViewHolderFactory = null)
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
            _elementViewHolderFactory = elementViewHolderFactory ?? new DefaultElementViewHolderFactory(Messenger.Default);
            this.WhenSourceChanges(Update);
        }

        private void Update()
        {
            var contentItem = _sourcePropertyFunc();
            var view = _targetPropertyFunc();

            if (contentItem == null || view == null)
                return;

            var contentItemContainer = view.FindViewById<ViewGroup>(Resource.Id.contentItem_container);
            if (contentItemContainer == null)
                throw new ArgumentException("ContentItemBinding requires a view with a ViewGroup with id contentItem_container");

            var context = view.Context;
            var inflater = LayoutInflater.From(context);

            _contentItemViewHolder = _elementViewHolderFactory.CreateContentItemViewHolder(context, inflater, contentItemContainer, contentItem);
            
            contentItemContainer.AddView(_contentItemViewHolder.View);
        }

        public override void Detach()
        {
            base.Detach();
            _contentItemViewHolder?.Dispose();
        }
    }
}