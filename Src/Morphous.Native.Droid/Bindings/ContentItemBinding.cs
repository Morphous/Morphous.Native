using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Messaging;
using Morphous.Native.Droid.Events;
using Morphous.Native.Droid.Factories;
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
        private readonly IMessenger _messenger;


        public ContentItemBinding(
            object source,
            Expression<Func<IContentItem>> sourcePropertyExpression,
            Expression<Func<View>> targetPropertyExpression,
            IElementViewHolderFactory elementViewHolderFactory = null,
            IMessenger messenger = null)
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
            _elementViewHolderFactory = elementViewHolderFactory ?? DefaultElementViewHolderFactory.Instance;
            _messenger = messenger ?? Messenger.Default;
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

            var contentItemView = GetTemplate(context, inflater, contentItemContainer, contentItem.Alternates);
            contentItemContainer.AddView(contentItemView);

            foreach (var zone in contentItem.Zones)
            {
                var zoneLayout = contentItemView.FindViewById<ViewGroup>(context.Resources.GetIdentifier(zone.Name, "id", context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementViewHolder = _elementViewHolderFactory.Create(context, inflater, zoneLayout, element, _messenger);
                        _elementViewHolders.Add(elementViewHolder);
                        zoneLayout.AddView(elementViewHolder.View);
                    }
                }
            }

            if (contentItem.DisplayType == "Summary" && contentItemView.Clickable)
            {
                contentItemView.Click += (object sender, EventArgs e) => _messenger.Send(new ContentItemSelectedMessage(contentItem));
            }
        }

        private static View GetTemplate(Context context, LayoutInflater inflater, ViewGroup parent, IList<string> alternates)
        {
            foreach (var alternate in alternates)
            {
                var layoutId = Application.Context.Resources.GetIdentifier(alternate.ToLower(), "layout", context.PackageName);
                if (layoutId > 0)
                {
                    return inflater.Inflate(layoutId, parent, false);
                }
            }

            throw new InflateException("Couldn't find any content item templates to inflate.");
        }

        public override void Detach()
        {
            base.Detach();
            foreach (var elementViewHolder in _elementViewHolders)
            {
                elementViewHolder.Dispose();
            }
        }
    }
}