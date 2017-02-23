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
            
            var context = view.Context;
            var inflater = LayoutInflater.From(context);

            foreach (var zone in contentItem.Zones)
            {
                var zoneLayout = view.FindViewById<ViewGroup>(context.Resources.GetIdentifier(zone.Name, "id", context.PackageName));

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