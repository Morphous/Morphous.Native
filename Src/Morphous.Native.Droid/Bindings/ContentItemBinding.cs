using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using Morphous.Native.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Morphous.Native.Droid.Bindings
{
    public class ContentItemBinding : Binding<IContentItem, View>
    {
        private readonly Func<IContentItem> _sourcePropertyFunc;
        private readonly Func<View> _targetPropertyFunc;
        private readonly List<ElementViewHolder> _elementViewHolders = new List<ElementViewHolder>();

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
            
            var context = view.Context;

            foreach (var zone in contentItem.Zones)
            {
                var zoneLayout = view.FindViewById<ViewGroup>(context.Resources.GetIdentifier(zone.Name, "id", context.PackageName));

                if (zoneLayout != null)
                {
                    foreach (var element in zone.Elements)
                    {
                        var elementViewHolder = new ElementViewHolder<IContentElement>(context, element);
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

        public abstract class ElementViewHolder : IDisposable
        {
            protected Context Context { get; }
            protected IList<Binding> Bindings { get; } = new List<Binding>();

            public ElementViewHolder(Context context)
            {
                Context = context;
            }

            private View _view;
            public View View
            {
                get
                {
                    if (_view == null)
                    {
                        _view = CreateView();
                        BindView(_view);
                    }

                    return _view;
                }
            }

            protected abstract View CreateView();

            protected virtual void BindView(View view)
            {
            }

            public virtual void Dispose()
            {
                foreach (var binding in Bindings)
                {
                    binding.Detach();
                }
            }
        }

        public class ElementViewHolder<TElement> : ElementViewHolder where TElement : class, IContentElement
        {
            protected TElement Element { get; }

            public ElementViewHolder(Context context, TElement element) : base(context)
            {
                Element = element;
            }

            private TextView _textView;

            protected override View CreateView()
            {
                var _textView = new TextView(Context);
                return _textView;
            }

            protected override void BindView(View view)
            {
                Bindings.Add(this.SetBinding(() => Element.Type, () => _textView.Text));
            }
        }
    }
}