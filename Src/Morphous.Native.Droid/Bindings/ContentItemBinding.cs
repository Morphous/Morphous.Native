using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
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
            _elementViewHolderFactory = elementViewHolderFactory ?? DefaultElementViewHolderFactory.Instance;
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

            var contentItemView = GetTemplate(context, inflater, contentItemContainer, contentItem);
            contentItemContainer.AddView(contentItemView);

            foreach (var zone in contentItem.Zones)
            {
                var zoneLayout = contentItemView.FindViewById<ViewGroup>(context.Resources.GetIdentifier(zone.Name, "id", context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementViewHolder = _elementViewHolderFactory.Create(context, inflater, zoneLayout, element);
                        _elementViewHolders.Add(elementViewHolder);
                        zoneLayout.AddView(elementViewHolder.View);
                    }
                }
            }
        }

        private static View GetTemplate(Context context, LayoutInflater inflater, ViewGroup parent, IContentItem contentItem)
        {
            var id = contentItem.Id;
            var contentType = contentItem.ContentType.ToLower();
            var displayType = contentItem.DisplayType.ToLower();

            var alternates = new string[]
            {
                $"contentitem_{id}",
                $"contentitem_{contentType}_{displayType}",
                $"contentitem_{contentType}",
                $"contentitem_{displayType}",
                $"contentitem",
            };

            foreach (var alternate in alternates)
            {
                var layoutId = Application.Context.Resources.GetIdentifier(alternate, "layout", context.PackageName);
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